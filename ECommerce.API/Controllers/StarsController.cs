using Microsoft.AspNetCore.Mvc;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Interface;
using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StarsController : ControllerBase
    {
        private readonly IProductUserRankRepository _productUserRankRepository;
        private readonly ILogger<StarsController> _logger;

        public StarsController(IProductUserRankRepository productUserRankRepository, ILogger<StarsController> logger)
        {
            this._productUserRankRepository = productUserRankRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetByProductId(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _productUserRankRepository.Where(x => x.ProductId == id, cancellationToken);
                if (result == null)
                {
                    return Ok(new ApiResult
                    {
                        Code = ResultCode.NotFound
                    });
                }

                return Ok(new ApiResult
                {
                    Code = ResultCode.Success,
                    ReturnData = result
                });
            }
            catch (Exception e)
            {
                 _logger.LogCritical(e, e.Message); return Ok(new ApiResult { Code = ResultCode.DatabaseError,Messages = new List<string> {  "اشکال در سمت سرور" }});
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBySumProductId(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _productUserRankRepository.Where(x => x.ProductId == id, cancellationToken);
                if (result == null)
                {
                    return Ok(new ApiResult
                    {
                        Code = ResultCode.NotFound
                    });
                }

                return Ok(new ApiResult
                {
                    Code = ResultCode.Success,
                    ReturnData = result.Sum(x => x.Stars)
                });
            }
            catch (Exception e)
            {
                 _logger.LogCritical(e, e.Message); return Ok(new ApiResult { Code = ResultCode.DatabaseError,Messages = new List<string> {  "اشکال در سمت سرور" }});
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _productUserRankRepository.Where(x => x.UserId == id, cancellationToken);
                if (result == null)
                {
                    return Ok(new ApiResult
                    {
                        Code = ResultCode.NotFound
                    });
                }

                return Ok(new ApiResult
                {
                    Code = ResultCode.Success,
                    ReturnData = result
                });
            }
            catch (Exception e)
            {
                 _logger.LogCritical(e, e.Message); return Ok(new ApiResult { Code = ResultCode.DatabaseError,Messages = new List<string> {  "اشکال در سمت سرور" }});
            }
        }

        [HttpPost]
        [Authorize(Roles = "Client,Admin,SuperAdmin")]
        public async Task<IActionResult> Post(ProductUserRank productUserRank, CancellationToken cancellationToken)
        {
            try
            {
                if (productUserRank == null)
                {
                    return Ok(new ApiResult
                    {
                        Code = ResultCode.BadRequest
                    });
                }

                var repetitiveProductUserRank = await _productUserRankRepository.GetByProductUser(productUserRank.ProductId, productUserRank.UserId, cancellationToken);
                if (repetitiveProductUserRank != null)
                {
                    repetitiveProductUserRank.Stars = productUserRank.Stars;
                    return Ok(new ApiResult
                    {
                        Code = ResultCode.Success,
                        ReturnData = await _productUserRankRepository.UpdateAsync(repetitiveProductUserRank, cancellationToken)
                    });
                }

                return Ok(new ApiResult
                {
                    Code = ResultCode.Success,
                    ReturnData = await _productUserRankRepository.AddAsync(productUserRank, cancellationToken)
                });
            }
            catch (Exception e)
            {
                 _logger.LogCritical(e, e.Message); return Ok(new ApiResult { Code = ResultCode.DatabaseError,Messages = new List<string> {  "اشکال در سمت سرور" }});
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
                 _logger.LogCritical(e, e.Message); return Ok(new ApiResult { Code = ResultCode.DatabaseError,Messages = new List<string> {  "اشکال در سمت سرور" }});
            }
        }
    }
}
