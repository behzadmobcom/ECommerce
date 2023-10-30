using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(ICategoryRepository categoryRepository, ILogger<CategoriesController> logger)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        
            if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
            var entity = await _categoryRepository.Search(paginationParameters, cancellationToken);
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
    public async Task<ActionResult<Category>> GetById(int id, CancellationToken cancellationToken)
    {
        
            var result = await _categoryRepository.GetByIdAsync(cancellationToken, id);
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

    [HttpGet]
    public async Task<IActionResult> GetParents(int productId, CancellationToken cancellationToken)
    {
        
            var result = await _categoryRepository.Parents(productId, cancellationToken);
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

    [HttpGet]
    public async Task<IActionResult> Search(string searchKeyword, CancellationToken cancellationToken)
    {
        
            var result = await _categoryRepository.Search(searchKeyword, cancellationToken);
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


    [HttpGet]
    public async Task<ActionResult<CategoryViewModel>> GetByUrl(string url, CancellationToken cancellationToken)
    {
        
            var result = await _categoryRepository.GetByUrl(url, cancellationToken);
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

    [HttpGet]
    public async Task<IActionResult> GetCategoriesByProduct(int productId, CancellationToken cancellationToken)
    {
        
            var categoryList = _categoryRepository.GetAll("Products")
                .Where(x => x.Products.Any(p => p.Id == productId));
            return Ok(await categoryList.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                ProductsId = x.Products.Select(product => product.Id).ToList(),
                ParentId = x.ParentId,
                Parent = x.Parent,
                Categories = x.Categories.Select(category => category.Id).ToList()
            }).ToListAsync(cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Post(Category category, CancellationToken cancellationToken)
    {
        
            if (category == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });
            category.Name = category.Name.Trim();

            var repetitiveCategory =
                await _categoryRepository.GetByName(category.Name, cancellationToken, category.ParentId);
            if (repetitiveCategory != null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"نام دسته تکراری است"}
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _categoryRepository.AddAsync(category, cancellationToken)
            });
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(Category category, CancellationToken cancellationToken)
    {
        
            await _categoryRepository.UpdateAsync(category, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }

    [HttpDelete]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        
            await _categoryRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }
}