using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PricesController : ControllerBase
{
    private readonly ILogger<PricesController> _logger;
    private readonly IPriceRepository _priceRepository;
    private readonly IHolooArticleRepository _holooArticleRepository;

    public PricesController(IPriceRepository priceRepository, ILogger<PricesController> logger, IHolooArticleRepository holooArticleRepository)
    {
        _priceRepository = priceRepository;
        _logger = logger;
        _holooArticleRepository = holooArticleRepository;
    }

    /// <summary>
    ///     Get All Price By Product Id with Pagination.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        
            if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
            var entity = await _priceRepository.Search(paginationParameters, cancellationToken);
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
    public async Task<ActionResult<Price>> GetById(int id, CancellationToken cancellationToken)
    {
        
            var result = await _priceRepository.GetByIdAsync(cancellationToken, id);
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
    public async Task<IActionResult> GetProductsPriceById(int id, CancellationToken cancellationToken)
    {
        
            var result = await _priceRepository.PriceOfProduct(id, cancellationToken);
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
    public async Task<IActionResult> Post(Price price, CancellationToken cancellationToken)
    {
        
            var messages = new List<string>();
            if (price == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });

            if (price.ArticleCode == null && price.Amount == 0)
                messages.Add("لطفا یا کد کالا وارد کنید یا مبلغ");

            messages.AddRange(await CheckPrice(price, cancellationToken));
            var holooPrice = await _holooArticleRepository.GetHolooPrice(price.ArticleCodeCustomer, price.SellNumber.Value);
            if (holooPrice.price == 0)
                messages.Add("شماره قیمت انتخاب شده فاقد مقدار می باشد");

            if(messages.Count > 0)
                return Ok(new ApiResult
                {
                    Messages = messages,
                    Code = ResultCode.BadRequest
                });

            var newPrice = await _priceRepository.AddAsync(price, cancellationToken);
            await _holooArticleRepository.SyncHolooWebId(newPrice.ArticleCodeCustomer, newPrice.ProductId, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = newPrice
            });
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(Price price, CancellationToken cancellationToken)
    {
        
            var messages = new List<string>();
            if (price.ArticleCode == null && price.Amount == 0)
                messages.Add("لطفا یا کد کالا وارد کنید یا مبلغ");

            messages.AddRange(await CheckPrice(price, cancellationToken));

            var HolooPrice = await _holooArticleRepository.GetHolooPrice(price.ArticleCodeCustomer, price.SellNumber.Value);

            if (HolooPrice.price == 0)
                messages.Add("شماره قیمت انتخاب شده فاقد مقدار می باشد");

            if (messages.Count > 0)
                return Ok(new ApiResult
                {
                    Messages = messages,
                    Code = ResultCode.BadRequest
                });

            await _priceRepository.UpdateAsync(price, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }

    [HttpDelete]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        
            await _priceRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }

    private async Task<List<string>> CheckPrice(Price price, CancellationToken cancellationToken)
    {
        var messages = new List<string>();
        var prices = await _priceRepository.PriceOfProduct(price.ProductId, cancellationToken);
        var repetitive = prices.Where(x => x.Amount == price.Amount
                                           && x.IsColleague == price.IsColleague
                                           && x.ColorId == price.ColorId
                                           && x.SizeId == price.SizeId);
        if (repetitive.Any() && !repetitive.Any(x => x.Id == price.Id)) messages.Add("مبلغ وارد شده تکراری است");
        if (prices.Any(x => x.MinQuantity <= price.MinQuantity
                            && x.MaxQuantity >= price.MinQuantity
                            && x.IsColleague == price.IsColleague
                            && x.ColorId == price.ColorId
                            && x.SizeId == price.SizeId))
            messages.Add("حداقل تعداد در بازه تعداد های قبلی این کالا است");
        if (prices.Any(x => x.MinQuantity <= price.MaxQuantity
                            && x.MaxQuantity >= price.MaxQuantity
                            && x.IsColleague == price.IsColleague
                            && x.ColorId == price.ColorId
                            && x.SizeId == price.SizeId))
            messages.Add("حداکثر تعداد در بازه تعداد های قبلی این کالا است");

        return messages;
    }
}