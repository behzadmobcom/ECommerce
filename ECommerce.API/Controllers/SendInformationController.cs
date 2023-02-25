using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SendInformationController : ControllerBase
{
    private readonly ILogger<BrandsController> _logger;
    private readonly ISendInformationRepository _sendInformationRepository;

    public SendInformationController(ISendInformationRepository sendInformationRepository,
        ILogger<BrandsController> logger)
    {
        _sendInformationRepository = sendInformationRepository;
        _logger = logger;
    }

    [HttpGet("GetByUserId/{id}")]
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<IActionResult> GetByUserId(int id, CancellationToken cancellationToken)
    {
        try
        {
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _sendInformationRepository.Where(x => x.UserId == id, cancellationToken)
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
                {Code = ResultCode.DatabaseError, Messages = new List<string> {"اشکال در سمت سرور"}});
        }
    }

    [HttpGet("GetById/{id}")]
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<ActionResult<SendInformation>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _sendInformationRepository.GetByIdAsync(cancellationToken, id);
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
                {Code = ResultCode.DatabaseError, Messages = new List<string> {"اشکال در سمت سرور"}});
        }
    }

    [HttpPost]
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<IActionResult> Post(SendInformation sendInformation, CancellationToken cancellationToken)
    {
        try
        {
            if (sendInformation == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });
            sendInformation.Address = sendInformation.Address.Trim();

            var repetitive = await _sendInformationRepository.Where(
                x => x.UserId == sendInformation.UserId && x.RecipientName.Equals(sendInformation.RecipientName) &&
                     x.Address.Equals(sendInformation.Address), cancellationToken);
            if (repetitive.Any())
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"آدرس تکراری است"},
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _sendInformationRepository.AddAsync(sendInformation, cancellationToken)
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
                {Code = ResultCode.DatabaseError, Messages = new List<string> {"اشکال در سمت سرور"}});
        }
    }

    [HttpPut]
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(SendInformation sendInformation, CancellationToken cancellationToken)
    {
        try
        {
            var repetitive = await _sendInformationRepository.Where(
                x => x.UserId == sendInformation.UserId && x.RecipientName.Equals(sendInformation.RecipientName) &&
                     x.Address.Equals(sendInformation.Address), cancellationToken);
            if (repetitive != null && repetitive.FirstOrDefault().Id != sendInformation.Id)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"آدرس تکراری است"}
                });

            await _sendInformationRepository.UpdateAsync(sendInformation, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
                {Code = ResultCode.DatabaseError, Messages = new List<string> {"اشکال در سمت سرور"}});
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _sendInformationRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
                {Code = ResultCode.DatabaseError, Messages = new List<string> {"اشکال در سمت سرور"}});
        }
    }
}