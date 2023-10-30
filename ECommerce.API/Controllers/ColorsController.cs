using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ColorsController : ControllerBase
{
    private readonly IColorRepository _colorRepository;
    private readonly ILogger<ColorsController> _logger;

    public ColorsController(IColorRepository colorGroupRepository, ILogger<ColorsController> logger)
    {
        _colorRepository = colorGroupRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        
            var colors = await _colorRepository.GetAll(cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = colors
            });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWithPagination([FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        
            if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
            var entity = await _colorRepository.Search(paginationParameters, cancellationToken);
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
    public async Task<ActionResult<Color>> GetById(int id, CancellationToken cancellationToken)
    {
        
            var result = await _colorRepository.GetByIdAsync(cancellationToken, id);
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
    public async Task<IActionResult> Post(Color color, CancellationToken cancellationToken)
    {
        
            if (color == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });
            color.Name = color.Name.Trim();

            var repetitiveColor = await _colorRepository.GetByName(color.Name, cancellationToken);
            if (repetitiveColor != null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"نام رنگ تکراری است"}
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _colorRepository.AddAsync(color, cancellationToken)
            });
       
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(Color color, CancellationToken cancellationToken)
    {
        
            await _colorRepository.UpdateAsync(color, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
       
    }

    [HttpDelete]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        
            await _colorRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
       
    }
}