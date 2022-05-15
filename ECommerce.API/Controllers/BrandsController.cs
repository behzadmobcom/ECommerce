using API.Interface;
using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BrandsController : ControllerBase
{
    private readonly IBrandRepository _brandRepository;
    private readonly ILogger<BrandsController> _logger;

    public BrandsController(IBrandRepository brandRepository, ILogger<BrandsController> logger)
    {
        _brandRepository = brandRepository;
        _logger = logger;
    }

    /// <summary>
    ///     Get All Brands.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _brandRepository.GetAll(cancellationToken);
         var brands =   result.ToList();
         brands.Insert(0,new Brand
            {
                Name = "بدون برند"
            });
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = brands
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
                {Code = ResultCode.DatabaseError, Messages = new List<string> {"اشکال در سمت سرور"}});
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWithPagination([FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
            var entity = await _brandRepository.Search(paginationParameters, cancellationToken);
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

            //var metadata = new
            //{
            //    entity.TotalCount,
            //    entity.PageSize,
            //    entity.CurrentPage,
            //    entity.TotalPages,
            //    entity.HasNext,
            //    entity.HasPrevious
            //};
            //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(new ApiResult
            {
                PaginationDetails = paginationDetails,
                Code = ResultCode.Success,
                ReturnData = entity
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
                {Code = ResultCode.DatabaseError, Messages = new List<string> {"اشکال در سمت سرور"}});
        }
    }

    [HttpGet]
    public async Task<ActionResult<Brand>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var x = User.Identity.Name;
            var result = await _brandRepository.GetByIdAsync(cancellationToken, id);
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
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
                {Code = ResultCode.DatabaseError, Messages = new List<string> {"اشکال در سمت سرور"}});
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Post(Brand brand, CancellationToken cancellationToken)
    {
        try
        {
            if (brand == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });
            brand.Name = brand.Name.Trim();

            var repetitiveBrand = await _brandRepository.GetByName(brand.Name, cancellationToken);
            if (repetitiveBrand != null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"نام برند تکراری است"}
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _brandRepository.AddAsync(brand, cancellationToken)
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
                {Code = ResultCode.DatabaseError, Messages = new List<string> {"اشکال در سمت سرور"}});
        }
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(Brand brand, CancellationToken cancellationToken)
    {
        try
        {
            var repetitive = await _brandRepository.GetByName(brand.Name, cancellationToken);
            if (repetitive != null && repetitive.Id != brand.Id)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"نام برند تکراری است"}
                });
            if (repetitive != null) _brandRepository.Detach(repetitive);
            await _brandRepository.UpdateAsync(brand, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
                {Code = ResultCode.DatabaseError, Messages = new List<string> {"اشکال در سمت سرور"}});
        }
    }

    [HttpDelete]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _brandRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
                {Code = ResultCode.DatabaseError, Messages = new List<string> {"اشکال در سمت سرور"}});
        }
    }
}