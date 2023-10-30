using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProductAttributesController : ControllerBase
{
    private readonly ILogger<ProductAttributesController> _logger;
    private readonly IProductAttributeRepository _productAttributeRepository;

    public ProductAttributesController(IProductAttributeRepository productAttributeRepository,
        ILogger<ProductAttributesController> logger)
    {
        _productAttributeRepository = productAttributeRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int groupId, CancellationToken cancellationToken)
    {
        
            var entity = await _productAttributeRepository.GetAll(cancellationToken);

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = entity
            });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAttributeByGroupId(int groupId, int productId,
        CancellationToken cancellationToken)
    {
        
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData =
                    await _productAttributeRepository.GetAllAttributeWithGroupId(groupId, productId, cancellationToken)
            });
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        
            if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
            var entity = await _productAttributeRepository.Search(paginationParameters, cancellationToken);
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
    public async Task<ActionResult<ProductAttribute>> GetById(int id, CancellationToken cancellationToken)
    {
        
            var result = await _productAttributeRepository.GetByIdAsync(cancellationToken, id);
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
    public async Task<IActionResult> Post(ProductAttribute productAttribute, CancellationToken cancellationToken)
    {
        
            if (productAttribute == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });
            productAttribute.Title = productAttribute.Title.Trim();

            var repetitiveProductAttribute =
                await _productAttributeRepository.GetByTitle(productAttribute.Title, cancellationToken);
            if (repetitiveProductAttribute != null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"نام خصوصیت تکراری است"}
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _productAttributeRepository.AddAsync(productAttribute, cancellationToken)
            });
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(ProductAttribute productAttribute, CancellationToken cancellationToken)
    {
        
            var repetitive = await _productAttributeRepository.GetByTitle(productAttribute.Title, cancellationToken);
            if (repetitive != null && repetitive.Id != productAttribute.Id)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"نام خصوصیت تکراری است"}
                });
            if (repetitive != null) _productAttributeRepository.Detach(repetitive);
            await _productAttributeRepository.UpdateAsync(productAttribute, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }

    [HttpDelete]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        
            await _productAttributeRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }
}