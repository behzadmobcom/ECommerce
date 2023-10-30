using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class StarsController : ControllerBase
{
    private readonly ILogger<StarsController> _logger;
    private readonly IProductUserRankRepository _productUserRankRepository;
    private readonly IProductRepository _productRepository;

    public StarsController(IProductUserRankRepository productUserRankRepository, ILogger<StarsController> logger, IProductRepository productRepository)
    {
        _productUserRankRepository = productUserRankRepository;
        _logger = logger;
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetByProductId(int id, CancellationToken cancellationToken)
    {
        
            var result = await _productUserRankRepository.Where(x => x.ProductId == id, cancellationToken);
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
    public async Task<IActionResult> GetBySumProductId(int id, CancellationToken cancellationToken)
    {
        
            var result = await _productUserRankRepository.Where(x => x.ProductId == id, cancellationToken);
            if (result == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.NotFound
                });

            var average = await _productUserRankRepository.GetBySumProduct(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = average
            });
    }

    [HttpGet]
    public async Task<IActionResult> GetByUserId(int id, CancellationToken cancellationToken)
    {
        
            var result = await _productUserRankRepository.Where(x => x.UserId == id, cancellationToken);
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
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<IActionResult> Post(ProductUserRank productUserRank, CancellationToken cancellationToken)
    {
        
            if (productUserRank == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });

            if (productUserRank.Stars < 0) return Ok(new ApiResult
            {
                Code = ResultCode.BadRequest,
                Messages = new List<string> { "مقدار وارد شده نادرست می‌باشد." }
            });

            var repetitiveProductUserRank = await _productUserRankRepository.GetByProductUser(productUserRank.ProductId,
                productUserRank.UserId, cancellationToken);
            if (repetitiveProductUserRank != null)
            {
                repetitiveProductUserRank.Stars = productUserRank.Stars;
                await _productUserRankRepository.UpdateAsync(repetitiveProductUserRank, cancellationToken);
            }
            else
            {
                await _productUserRankRepository.AddAsync(productUserRank, cancellationToken);
            }

            var productUserRanks = await _productUserRankRepository.GetBySumProduct(productUserRank.ProductId, cancellationToken);
            var product = await _productRepository.GetByIdAsync(cancellationToken, productUserRank.ProductId);
            product.Star = productUserRanks;
            await _productRepository.UpdateAsync(product, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = productUserRanks
            });
    }

    [HttpDelete]
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        
            await _productUserRankRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }
}