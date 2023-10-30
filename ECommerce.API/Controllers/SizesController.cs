using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SizesController : ControllerBase
{
    private readonly ILogger<SizesController> _logger;
    private readonly ISizeRepository _sizeRepository;

    public SizesController(ISizeRepository sizeRepository, ILogger<SizesController> logger)
    {
        _sizeRepository = sizeRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        
            if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
            var entity = await _sizeRepository.Search(paginationParameters, cancellationToken);
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
    public async Task<ActionResult<Size>> GetById(int id, CancellationToken cancellationToken)
    {
        
            var result = await _sizeRepository.GetByIdAsync(cancellationToken, id);
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
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Post(Size size, CancellationToken cancellationToken)
    {
        
            if (size == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });
            size.Name = size.Name.Trim();

            var repetitiveSize = await _sizeRepository.GetByName(size.Name, cancellationToken);
            if (repetitiveSize != null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"سایز تکراری است"}
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _sizeRepository.AddAsync(size, cancellationToken)
            });
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(Size size, CancellationToken cancellationToken)
    {
        
            await _sizeRepository.UpdateAsync(size, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }

    [HttpDelete]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        
            await _sizeRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }
}