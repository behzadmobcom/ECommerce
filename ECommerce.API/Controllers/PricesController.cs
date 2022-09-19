using API.Interface;
using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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
        try
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
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpGet]
    public async Task<ActionResult<Price>> GetById(int id, CancellationToken cancellationToken)
    {
        try
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
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsPriceById(int id, CancellationToken cancellationToken)
    {
        try
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
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Post(Price price, CancellationToken cancellationToken)
    {
        try
        {
            if (price == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });

            if (price.ArticleCode == null && price.Amount == 0)
            {
                return Ok(new ApiResult
                {
                    Messages = new List<string> { "لطفا یا کد کالا وارد کنید یا مبلغ" },
                    Code = ResultCode.Error
                });
            }

            var messages = await CheckPrice(price, cancellationToken);
            var HolooPrice = await _holooArticleRepository.GetHolooPrice(price.ArticleCode, price.SellNumber.Value);
            if (HolooPrice.price == 0)
                messages.Add("شماره قیمت انتخاب شده فاقد مقدار می باشد");
            if (messages.Count == 0)
            {
                var newPrice = await _priceRepository.AddAsync(price, cancellationToken);
                await _holooArticleRepository.SyncHolooWebId(newPrice.ArticleCodeCustomer, newPrice.ProductId, cancellationToken);
                return Ok(new ApiResult
                {
                    Code = ResultCode.Success,
                    ReturnData = newPrice
                });
            }
                
            return Ok(new ApiResult
            {
                Messages = messages,
                Code = ResultCode.BadRequest
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(Price price, CancellationToken cancellationToken)
    {
        try
        {
            if (price.ArticleCode == null && price.Amount == 0)
            {
                return Ok(new ApiResult
                {
                    Messages =new List<string> {"لطفا یا کد کالا وارد کنید یا مبلغ"},
                    Code = ResultCode.Error
                });
            }
            var messages = await CheckPrice(price, cancellationToken);
            var HolooPrice = await _holooArticleRepository.GetHolooPrice(price.ArticleCode, price.SellNumber.Value);
            if (HolooPrice.price == 0)
                messages.Add("شماره قیمت انتخاب شده فاقد مقدار می باشد");
            
            if (messages.Count == 0)
            {
                await _priceRepository.UpdateAsync(price, cancellationToken);
                return Ok(new ApiResult
                {
                    Code = ResultCode.Success
                });
            }

            return Ok(new ApiResult
            {
                Messages = messages,
                Code = ResultCode.BadRequest
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpDelete]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _priceRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
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