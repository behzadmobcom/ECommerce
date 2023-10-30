using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class DiscountsController : ControllerBase
{
    private readonly IHolooArticleRepository _articleRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly ILogger<DiscountsController> _logger;

    public DiscountsController(IDiscountRepository discountRepository, ILogger<DiscountsController> logger,
                                IHolooArticleRepository articleRepository)
    {
        _discountRepository = discountRepository;
        _logger = logger;
        _articleRepository = articleRepository;
    }

    [HttpGet]
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        
            if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
            var entity = await _discountRepository.Search(paginationParameters, cancellationToken);
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
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<ActionResult<Discount>> GetById(int id, CancellationToken cancellationToken)
    {
        
            var result = await _discountRepository.GetByIdAsync(cancellationToken, id);
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
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<ActionResult<Discount>> GetByCode(string code, CancellationToken cancellationToken)
    {
        
            var result = await _discountRepository.GetByCode(code, cancellationToken);
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
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<ActionResult<Discount>> GetLast(CancellationToken cancellationToken)
    {
        
            var result = await _discountRepository.GetLast(cancellationToken);
            if (result == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.NotFound
                });
            foreach (var productPrices in result.Prices)
                if (productPrices.SellNumber != null && productPrices.SellNumber != Price.HolooSellNumber.خالی && productPrices.ArticleCode != null)
                {

                    var article = await _articleRepository.GetHolooPrice(productPrices.ArticleCodeCustomer,
                         productPrices.SellNumber!.Value);

                    productPrices.Amount = article.price / 10;
                    productPrices.Exist = (double)article.exist;
                }
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = result
            });
    }


    [HttpGet]
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<ActionResult<Discount>> GetWithTime(CancellationToken cancellationToken)
    {
        
            var result = await _discountRepository.GetWithTime(cancellationToken);
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
    [Authorize(Roles = "Admin,SuperAdmin")]
    public ActionResult<bool> ActiveDiscount(int id)
    {
        
            var result =  _discountRepository.Active(id);

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = result
            });
    }

    [HttpPost]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Post(Discount discount, CancellationToken cancellationToken)
    {
        
            if (discount == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });
            discount.Name = discount.Name.Trim();

            var repetitiveName = await _discountRepository.GetByName(discount.Name, cancellationToken);
            if (repetitiveName != null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"نام تخفیف تکراری است"}
                });

            var repetitiveCode = await _discountRepository.GetByCode(discount.Code, cancellationToken);
            if (repetitiveCode != null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"کد تخفیف تکراری است"}
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _discountRepository.AddAsync(discount, cancellationToken)
            });
       
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(Discount discount, CancellationToken cancellationToken)
    {
        
            await _discountRepository.UpdateAsync(discount, cancellationToken);

            var repetitiveCode = await _discountRepository.GetByCode(discount.Code, cancellationToken);
            if (repetitiveCode != null && repetitiveCode.Id != discount.Id)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> { "کد تخفیف تکراری است" }
                });

            var repetitiveName = await _discountRepository.GetByName(discount.Name, cancellationToken);
            if (repetitiveName != null && repetitiveCode.Id != discount.Id)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> { "نام تخفیف تکراری است" }
                });
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
       
    }

    [HttpDelete]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        
            await _discountRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
       
    }
}