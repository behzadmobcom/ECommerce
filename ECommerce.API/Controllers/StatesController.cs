using API.Interface;
using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

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
        try
        {
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _stateRepository.GetAll("Cities").ToListAsync(cancellationToken)
            });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Database Error " + e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<State>> GetById(int id, CancellationToken cancellationToken)
    {
        try
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
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult {Code = ResultCode.DatabaseError});
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(State state, CancellationToken cancellationToken)
    {
        try
        {
            if (state == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
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
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult {Code = ResultCode.DatabaseError});
        }
    }

    [HttpPut]
    public async Task<ActionResult<bool>> Put(State state, CancellationToken cancellationToken)
    {
        try
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
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult {Code = ResultCode.DatabaseError});
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _stateRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult {Code = ResultCode.DatabaseError});
        }
    }
}