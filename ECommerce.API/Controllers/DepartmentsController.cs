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
    public class DepartmentsController : ControllerBase
    {

        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILogger<DepartmentsController> _logger;

        public DepartmentsController(IDepartmentRepository departmentRepository, ILogger<DepartmentsController> logger)
        {
            this._departmentRepository = departmentRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParameters , CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
                var entity = await _departmentRepository.Search(paginationParameters, cancellationToken);
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
        public async Task<ActionResult<Department>> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _departmentRepository.GetByIdAsync(cancellationToken,id);
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
        public async Task<IActionResult> Post(Department department, CancellationToken cancellationToken)
        {
            try
            {
                if (department == null)
                {
                    return Ok(new ApiResult
                    {
                        Code = ResultCode.BadRequest
                    });
                }
                department.Title = department.Title.Trim();

                var repetitiveDepartment = await _departmentRepository.GetByTitle(department.Title, cancellationToken);
                if (repetitiveDepartment != null)
                {
                    return Ok(new ApiResult
                    {
                        Code = ResultCode.Repetitive,
                        Messages = new List<string> { "عنوان دپارتمان تکراری است" }
                    });
                }

                return Ok(new ApiResult
                {
                    Code = ResultCode.Success,
                    ReturnData = await _departmentRepository.AddAsync(department, cancellationToken)
                });
            }
            catch (Exception e)
            {
                 _logger.LogCritical(e, e.Message); return Ok(new ApiResult {Code = ResultCode.DatabaseError});
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult<bool>> Put(Department department, CancellationToken cancellationToken)
        {
            try
            {
                var repetitiveDepartment = await _departmentRepository.GetByTitle(department.Title, cancellationToken);
                if (repetitiveDepartment != null && repetitiveDepartment.Id != department.Id)
                {
                    return Ok(new ApiResult
                    {
                        Code = ResultCode.Repetitive,
                        Messages = new List<string> { "نام دپارتمان تکراری است" }
                    });
                }
                if (repetitiveDepartment != null) { _departmentRepository.Detach(repetitiveDepartment); }
                await _departmentRepository.UpdateAsync(department, cancellationToken);
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
                await _departmentRepository.DeleteAsync(id, cancellationToken);
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
