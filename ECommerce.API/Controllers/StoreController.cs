using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
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
    public class StoreController : ControllerBase
    {

        private readonly IStoreRepository _storeRepository;
        private readonly ILogger<StoreController> _logger;

        public StoreController(IStoreRepository storeRepository, ILogger<StoreController> logger)
        {
            this._storeRepository = storeRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParameters , CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
                var entity = await _storeRepository.Search(paginationParameters, cancellationToken);
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
            catch (Exception e)
            {
                 _logger.LogCritical(e, e.Message); return Ok(new ApiResult {Code = ResultCode.DatabaseError});
            }
        }

        [HttpGet]
        public async Task<ActionResult<Store>> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _storeRepository.GetByIdAsync(cancellationToken,id);
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
                 _logger.LogCritical(e, e.Message); return Ok(new ApiResult {Code = ResultCode.DatabaseError});
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Post(Store store, CancellationToken cancellationToken)
        {
            try
            {
                if (store == null)
                {
                    return Ok(new ApiResult
                    {
                        Code = ResultCode.BadRequest
                    });
                }
                store.Name = store.Name.Trim();

                var repetitiveStore = await _storeRepository.GetByName(store.Name, cancellationToken);
                if (repetitiveStore != null)
                {
                    return Ok(new ApiResult
                    {
                        Code = ResultCode.Repetitive,
                        Messages = new List<string> { "نام انبار تکراری است" }
                    });
                }

                return Ok(new ApiResult
                {
                    Code = ResultCode.Success,
                    ReturnData = await _storeRepository.AddAsync(store, cancellationToken)
                });
            }
            catch (Exception e)
            {
                 _logger.LogCritical(e, e.Message); return Ok(new ApiResult {Code = ResultCode.DatabaseError});
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult<bool>> Put(Store store, CancellationToken cancellationToken)
        {
            try
            {
                var repetitive = await _storeRepository.GetByName(store.Name, cancellationToken);
                if (repetitive != null && repetitive.Id != store.Id)
                {
                    return Ok(new ApiResult
                    {
                        Code = ResultCode.Repetitive,
                        Messages = new List<string> { "نام انبار تکراری است" }
                    });
                }
                if (repetitive != null) { _storeRepository.Detach(repetitive); }
                await _storeRepository.UpdateAsync(store, cancellationToken);
                return Ok(new ApiResult
                {
                    Code = ResultCode.Success
                });
            }
            catch (Exception e)
            {
                 _logger.LogCritical(e, e.Message); return Ok(new ApiResult {Code = ResultCode.DatabaseError});
            }
        }

        [HttpDelete]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _storeRepository.DeleteAsync(id, cancellationToken);
                return Ok(new ApiResult
                {
                    Code = ResultCode.Success
                });
            }
            catch (Exception e)
            {
                 _logger.LogCritical(e, e.Message); return Ok(new ApiResult {Code = ResultCode.DatabaseError});
            }
        }
    }
}
