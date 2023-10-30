using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProductAttributeGroupsController : ControllerBase
{
    private readonly ILogger<ProductAttributeGroupsController> _logger;
    private readonly IProductAttributeGroupRepository _productAttributeGroupRepository;

    public ProductAttributeGroupsController(IProductAttributeGroupRepository productAttributeGroupRepository,
        ILogger<ProductAttributeGroupsController> logger)
    {
        _productAttributeGroupRepository = productAttributeGroupRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        
            if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
            var entity = await _productAttributeGroupRepository.Search(paginationParameters, cancellationToken);
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
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _productAttributeGroupRepository.GetAll(cancellationToken)
            });
    }

    [HttpGet]
    public async Task<ActionResult<ProductAttributeGroup>> GetByProductId(int productId,
        CancellationToken cancellationToken)
    {
        
            var result =
                await _productAttributeGroupRepository.GetAllAttributeWithProductId(productId, cancellationToken);
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
    public async Task<ActionResult<ProductAttributeGroup>> GetById(int id, CancellationToken cancellationToken)
    {
        
            var result = await _productAttributeGroupRepository.GetByIdAsync(cancellationToken, id);
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
    public async Task<IActionResult> Post(ProductAttributeGroup productAttributeGroup,
        CancellationToken cancellationToken)
    {
        
            if (productAttributeGroup == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });
            productAttributeGroup.Name = productAttributeGroup.Name.Trim();

            var repetitiveName =
                await _productAttributeGroupRepository.GetByName(productAttributeGroup.Name, cancellationToken);
            if (repetitiveName != null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"نام خصوصیت تکراری است"}
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _productAttributeGroupRepository.AddAsync(productAttributeGroup, cancellationToken)
            });
    }

    [HttpPost]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> AddWithAttributeValue(List<ProductAttributeGroup> productAttributeGroups,
        [FromQuery] int ProductId, CancellationToken cancellationToken)
    {
        
            if (productAttributeGroups == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData =
                    await _productAttributeGroupRepository.AddWithAttributeValue(productAttributeGroups, ProductId,
                        cancellationToken)
            });
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(ProductAttributeGroup productAttributeGroup,
        CancellationToken cancellationToken)
    {
        
            var repetitive =
                await _productAttributeGroupRepository.GetByName(productAttributeGroup.Name, cancellationToken);
            if (repetitive != null && repetitive.Id != productAttributeGroup.Id)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"نام خصوصیت تکراری است"}
                });
            if (repetitive != null) _productAttributeGroupRepository.Detach(repetitive);
            await _productAttributeGroupRepository.UpdateAsync(productAttributeGroup, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }

    [HttpDelete]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        
            await _productAttributeGroupRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }
}