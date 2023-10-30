using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly ILogger<DepartmentsController> _logger;

    public DepartmentsController(IDepartmentRepository departmentRepository, ILogger<DepartmentsController> logger)
    {
        _departmentRepository = departmentRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
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

    [HttpGet]
    public async Task<ActionResult<Department>> GetById(int id, CancellationToken cancellationToken)
    {
        
            var result = await _departmentRepository.GetByIdAsync(cancellationToken, id);
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
    public async Task<ActionResult<Department>> GetAll(CancellationToken cancellationToken)
    {
        
            var result = await _departmentRepository.GetAll(cancellationToken);
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
    public async Task<IActionResult> Post(Department department, CancellationToken cancellationToken)
    {
        
            if (department == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });
            department.Title = department.Title.Trim();

            var repetitiveDepartment = await _departmentRepository.GetByTitle(department.Title, cancellationToken);
            if (repetitiveDepartment != null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"عنوان دپارتمان تکراری است"}
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _departmentRepository.AddAsync(department, cancellationToken)
            });
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(Department department, CancellationToken cancellationToken)
    {
        
            var repetitiveDepartment = await _departmentRepository.GetByTitle(department.Title, cancellationToken);
            if (repetitiveDepartment != null && repetitiveDepartment.Id != department.Id)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"نام دپارتمان تکراری است"}
                });
            if (repetitiveDepartment != null) _departmentRepository.Detach(repetitiveDepartment);
            await _departmentRepository.UpdateAsync(department, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }

    [HttpDelete]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        
            await _departmentRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }
}