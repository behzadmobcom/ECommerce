using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.HolooEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PaymentMethodsController : ControllerBase
{
    private readonly IHolooAccountNumberRepository _accountNumberRepository;
    private readonly ILogger<PaymentMethodsController> _logger;

    private readonly IPaymentMethodRepository _paymentMethodRepository;

    public PaymentMethodsController(IPaymentMethodRepository discountRepository,
        IHolooAccountNumberRepository accountNumberRepository, ILogger<PaymentMethodsController> logger)
    {
        _paymentMethodRepository = discountRepository;
        _accountNumberRepository = accountNumberRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        
            if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
            var entity = await _paymentMethodRepository.Search(paginationParameters, cancellationToken);
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
    public async Task<ActionResult<PaymentMethod>> GetById(int id, CancellationToken cancellationToken)
    {
        
            var result = await _paymentMethodRepository.GetByIdAsync(cancellationToken, id);
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
    public async Task<IActionResult> Post(PaymentMethod paymentMethod, CancellationToken cancellationToken)
    {
        
            if (paymentMethod == null)
            {
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });
                ;
            }

            paymentMethod.AccountNumber = paymentMethod.AccountNumber.Trim();

            var repetitiveAccountNumber =
                await _paymentMethodRepository.GetByAccountNumber(paymentMethod.AccountNumber, cancellationToken);
            if (repetitiveAccountNumber != null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"شماره حساب تکراری است"}
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _paymentMethodRepository.AddAsync(paymentMethod, cancellationToken)
            });
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(PaymentMethod paymentMethod, CancellationToken cancellationToken)
    {
        
            var repetitive =
                await _paymentMethodRepository.GetByAccountNumber(paymentMethod.AccountNumber, cancellationToken);
            if (repetitive != null && repetitive.Id != paymentMethod.Id)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"شماره حساب تکراری است"}
                });
            if (repetitive != null) _paymentMethodRepository.Detach(repetitive);
            await _paymentMethodRepository.UpdateAsync(paymentMethod, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }

    [HttpDelete]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        
            await _paymentMethodRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }

    [HttpGet]
    public async Task<IActionResult> GetHolooAccountNumbers(CancellationToken cancellationToken)
    {
        
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _accountNumberRepository.GetAll(cancellationToken)
            });
    }

    [HttpGet]
    public async Task<ActionResult<HolooAccountNumber>> GetHolooAccountNumberById(string key,
        CancellationToken cancellationToken)
    {
        
            var result = await _accountNumberRepository.GetByAccountNumberAndBankCode(key, cancellationToken);
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
    public async Task<IActionResult> ConvertHolooToSunflower(CancellationToken cancellationToken)
    {
        
            var paymentMethods = (await _accountNumberRepository.GetAll(cancellationToken))!.Select(x =>
                new PaymentMethod
                {
                    AccountNumber = x.Account_N,
                    BankCode = x.Bank_Code,
                    BrunchName = x.Branch_Name,
                    BankName = x.Branch_Name
                });
            var result = await _paymentMethodRepository.AddAll(paymentMethods, cancellationToken);
            if (result == 0)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest,
                    Messages = new List<string> {"افزودن اتوماتیک به مشکل برخورد کرد"}
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }
}