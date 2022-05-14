using API.Interface;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class HolooFactorController : ControllerBase
{
    private readonly IHolooABailRepository _aBailRepository;
    private readonly IHolooFBailRepository _fBailRepository;
    private readonly ILogger<FactorViewModel> _logger;

    public HolooFactorController(IHolooFBailRepository fBailRepository, IHolooABailRepository aBailRepository,
        ILogger<FactorViewModel> logger)
    {
        _fBailRepository = fBailRepository;
        _aBailRepository = aBailRepository;
        _logger = logger;
    }

    [HttpPost]
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<IActionResult> Post(FactorViewModel factor, CancellationToken cancellationToken)
    {
        try
        {
            if (factor == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });

            factor.HolooFBail.Fac_Type = "P";
            var repetitiveBrand = await _fBailRepository.Add(factor.HolooFBail, cancellationToken);
            if (repetitiveBrand != null)
            {
                for (var index = 0; index < factor.HolooABails.Count; index++)
                {
                    factor.HolooABails[index].Fac_Code = repetitiveBrand;
                    factor.HolooABails[index].Fac_Type = "P";
                }

                await _aBailRepository.Add(factor.HolooABails, cancellationToken);
                return Ok(new ApiResult
                {
                    Code = ResultCode.Success
                });
            }

            return Ok(new ApiResult
            {
                Code = ResultCode.Error,
                Messages = new List<string> {"مشکل در ثبت فاکتور"}
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