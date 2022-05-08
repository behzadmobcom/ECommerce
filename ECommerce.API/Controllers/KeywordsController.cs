using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Interface;
using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class KeywordsController : ControllerBase
    {
        private readonly IKeywordRepository _keywordRepository;
        private readonly ILogger<KeywordsController> _logger;

        public KeywordsController(IKeywordRepository keywordRepository, ILogger<KeywordsController> logger)
        {
            this._keywordRepository = keywordRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParameters , CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
                var entity = await _keywordRepository.Search(paginationParameters, cancellationToken);
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
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(new ApiResult
                {
                    Code = ResultCode.Success,
                    ReturnData =await _keywordRepository.GetAll(cancellationToken)
                });
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, e.Message); return Ok(new ApiResult { Code = ResultCode.DatabaseError });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetKeywordsByProductId(int id,CancellationToken cancellationToken)
        {
            try
            {
                var keywordList = await _keywordRepository.GetByProductId(id, cancellationToken);
                return Ok(new ApiResult
                {
                    Code = ResultCode.Success,
                    ReturnData = keywordList
                });
            }
            catch (Exception e)
            {
                 _logger.LogCritical(e, e.Message); return Ok(new ApiResult {Code = ResultCode.DatabaseError});
            }
        }

        [HttpGet]
        public async Task<ActionResult<Keyword>> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _keywordRepository.GetByIdAsync(cancellationToken,id);
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
        public async Task<IActionResult> Post(Keyword keywords, CancellationToken cancellationToken)
        {
            try
            {
                if (keywords == null)
                {
                    return Ok(new ApiResult
                    {
                        Code = ResultCode.BadRequest
                    });
                }
                keywords.KeywordText = keywords.KeywordText.Trim();

                var repetitiveCategory = await _keywordRepository.GetByKeywordText(keywords.KeywordText, cancellationToken);
                if (repetitiveCategory != null)
                {
                    return Ok(new ApiResult
                    {
                        Code = ResultCode.Repetitive,
                        Messages = new List<string> { "کلمه کلیدی تکراری است" }
                    });
                }

                return Ok(new ApiResult
                {
                    Code = ResultCode.Success,
                    ReturnData = await _keywordRepository.AddAsync(keywords, cancellationToken)
                });
            }
            catch (Exception e)
            {
                 _logger.LogCritical(e, e.Message); return Ok(new ApiResult {Code = ResultCode.DatabaseError});
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult<bool>> Put(Keyword keyword, CancellationToken cancellationToken)
        {
            try
            {
                var repetitive = await _keywordRepository.GetByKeywordText(keyword.KeywordText, cancellationToken);
                if (repetitive != null && repetitive.Id != keyword.Id)
                {
                    return Ok(new ApiResult
                    {
                        Code = ResultCode.Repetitive,
                        Messages = new List<string> { "کلمه کلیدی تکراری است" }
                    });
                }
                if (repetitive != null) { _keywordRepository.Detach(repetitive); }
                await _keywordRepository.UpdateAsync(keyword, cancellationToken);
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
                await _keywordRepository.DeleteAsync(id, cancellationToken);
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
