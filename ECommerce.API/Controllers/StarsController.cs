using API.Interface;
using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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
        try
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
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetBySumProductId(int id, CancellationToken cancellationToken)
    {
        try
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
                ReturnData = result.Sum(x => x.Stars)
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetByUserId(int id, CancellationToken cancellationToken)
    {
        try
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
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<IActionResult> Post(ProductUserRank productUserRank, CancellationToken cancellationToken)
    {
        try
        {
            if (productUserRank == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
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

            var productUserRanks =await _productUserRankRepository.GetBySumProduct(productUserRank.ProductId, cancellationToken);
            var product = await _productRepository.GetByIdAsync(cancellationToken, productUserRank.ProductId);
            product.Star = productUserRanks;
            await _productRepository.UpdateAsync(product, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = productUserRanks
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpDelete]
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _productUserRankRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }
}