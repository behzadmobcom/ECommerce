using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class MessagesController : ControllerBase
{
    private readonly ILogger<MessagesController> _logger;
    private readonly IMessageRepository _messageRepository;

    public MessagesController(IMessageRepository messageRepository, ILogger<MessagesController> logger)
    {
        _messageRepository = messageRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        
            if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
            var entity = await _messageRepository.Search(paginationParameters, cancellationToken);
            var paginationDetails = new PaginationDetails
            {
                TotalCount = entity.TotalCount,
                PageSize = entity.PageSize,
                CurrentPage = entity.CurrentPage,
                TotalPages = entity.TotalPages,
                HasNext = entity.HasNext,
                HasPrevious = entity.HasPrevious,
                Search = paginationParameters.Search
            };
            return Ok(new ApiResult
            {
                PaginationDetails = paginationDetails,
                Code = ResultCode.Success,
                ReturnData = entity
            });
    }

    [HttpGet]
    public async Task<ActionResult<Message>> GetById(int id, CancellationToken cancellationToken)
    {
        
            var result = await _messageRepository.GetByIdAsync(cancellationToken, id);
            if (result == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.NotFound
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = result
            });
    }

    [HttpPost]
    public async Task<IActionResult> Post(Message message, CancellationToken cancellationToken)
    {
        
            if (message == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });

            message.DateTime = DateTime.Now;
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _messageRepository.AddAsync(message, cancellationToken)
            });
    }

    [HttpPut]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<ActionResult<bool>> Put(Message message, CancellationToken cancellationToken)
    {
        
            await _messageRepository.UpdateAsync(message, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }

    [HttpDelete]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        
            await _messageRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }
}