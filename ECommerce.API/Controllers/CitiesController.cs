using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CitiesController : ControllerBase
{
    private readonly ICityRepository _cityRepository;
    private readonly ILogger<CitiesController> _logger;

    public CitiesController(ICityRepository cityRepository, ILogger<CitiesController> logger)
    {
        _cityRepository = cityRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = (await _cityRepository.GetAll(cancellationToken)).OrderBy(x => x.Name)
            });
       
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWithPagination([FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        
            if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
            var entity = await _cityRepository.Search(paginationParameters, cancellationToken);
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
    public async Task<IActionResult> GetByStateId(int id, CancellationToken cancellationToken)
    {
        
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = (await _cityRepository.Where(x => x.StateId == id, cancellationToken)).OrderBy(x => x.Name)
            });
    }

    [HttpGet]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _cityRepository.GetByIdAsync(cancellationToken, id)
            });
       
    }

    [HttpPost]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Post(City city, CancellationToken cancellationToken)
    {
        
            if (city == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });
            city.Name = city.Name.Trim();

            var repetitiveCity = await _cityRepository.GetByName(city.Name, cancellationToken);
            if (repetitiveCity != null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"نام شهر تکراری است"}
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _cityRepository.AddAsync(city, cancellationToken)
            });
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(City city, CancellationToken cancellationToken)
    {
        
            await _cityRepository.UpdateAsync(city, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }

    [HttpDelete]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        
            await _cityRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }
}