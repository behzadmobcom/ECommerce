using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerce.API.Repository;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class StatesController : ControllerBase
{
    private readonly ILogger<StatesController> _logger;
    private readonly IStateRepository _stateRepository;

    public StatesController(IStateRepository stateRepository, ILogger<StatesController> logger)
    {
        _stateRepository = stateRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _stateRepository.GetAll("Cities").OrderBy(x => x.Name).ToListAsync(cancellationToken)
            });
       
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = (await _stateRepository.GetAll(cancellationToken)).OrderBy(x => x.Name)
            });
       
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWithPagination([FromQuery] PaginationParameters paginationParameters,
       CancellationToken cancellationToken)
    {
        
            if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
            var entity = await _stateRepository.Search(paginationParameters, cancellationToken);
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
    public async Task<ActionResult<State>> GetById(int id, CancellationToken cancellationToken)
    {
        
            var result = await _stateRepository.GetByIdAsync(cancellationToken, id);
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
    public async Task<IActionResult> Post(State state, CancellationToken cancellationToken)
    {
        
            if (state == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest,
                      ReturnData = await _stateRepository.GetAll(cancellationToken)
                });
            state.Name = state.Name.Trim();

            var repetitiveState = await _stateRepository.GetByName(state.Name, cancellationToken);
            if (repetitiveState != null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"نام استان تکراری است"}
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _stateRepository.AddAsync(state, cancellationToken)
            });
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(State state, CancellationToken cancellationToken)
    {
        
            var repetitive = await _stateRepository.GetByName(state.Name, cancellationToken);
            if (repetitive != null && repetitive.Id != state.Id)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"نام استان تکراری است"}
                });
            if (repetitive != null) _stateRepository.Detach(repetitive);
            await _stateRepository.UpdateAsync(state, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }

    [HttpDelete]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        
            await _stateRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }
}