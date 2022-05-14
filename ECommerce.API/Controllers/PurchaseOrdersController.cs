using API.Interface;
using ECommerce.Application.Commands.Purchase;
using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PurchaseOrdersController : ControllerBase
{
    private readonly IHolooArticleRepository _articleRepository;
    private readonly ILogger<PurchaseOrdersController> _logger;
    private readonly IProductRepository _productRepository;
    private readonly IPurchaseOrderDetailRepository _purchaseOrderDetailRepository;
    private readonly IPurchaseOrderRepository _purchaseOrderRepository;

    public PurchaseOrdersController(IPurchaseOrderRepository discountRepository,
        IPurchaseOrderDetailRepository purchaseOrderDetailRepository, IProductRepository productRepository,
        ILogger<PurchaseOrdersController> logger, IHolooArticleRepository articleRepository)
    {
        _purchaseOrderRepository = discountRepository;
        _purchaseOrderDetailRepository = purchaseOrderDetailRepository;
        _productRepository = productRepository;
        _logger = logger;
        _articleRepository = articleRepository;
    }

    [HttpGet]
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
            var entity = await _purchaseOrderRepository.Search(paginationParameters, cancellationToken);
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
            return Ok(new ApiResult {Code = ResultCode.DatabaseError});
        }
    }

    [HttpGet]
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<ActionResult<PurchaseOrder>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _purchaseOrderRepository.GetByIdAsync(cancellationToken, id);
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

    [HttpGet]
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<ActionResult<PurchaseOrderViewModel>> UserCart(int userId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _purchaseOrderRepository.GetProductListByUserId(userId, cancellationToken);
            if (result == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Success,
                    ReturnData = new List<PurchaseOrderViewModel>()
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
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<IActionResult> Post(CreatePurchaseCommand createPurchaseCommand,
        CancellationToken cancellationToken)
    {
        try
        {
            if (createPurchaseCommand == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });

            var purchaseOrder = new PurchaseOrder();
            var purchaseOrderDetail = new PurchaseOrderDetail();
            var product = await _productRepository.GetProductByIdWithInclude(createPurchaseCommand.ProductId)
                .FirstOrDefaultAsync(cancellationToken);

            //var colleaguePrice = product.Prices.Where(x => x.IsColleague == createPurchaseCommand.IsColleague ).ToList();
            //var minPrice = colleaguePrice.Any()
            //    ? colleaguePrice.Where(x => x.MinQuantity <= createPurchaseCommand.Quantity).ToList()
            //    : product.Prices.Where(x => x.MinQuantity <= createPurchaseCommand.Quantity && x.ArticleCode == createPurchaseCommand.ArticleCode).ToList();
            //var maxPrice = minPrice.Any() ? minPrice.FirstOrDefault(x => x.MaxQuantity >= createPurchaseCommand.Quantity) :
            //    product.Prices.FirstOrDefault(x => x.MaxQuantity >= createPurchaseCommand.Quantity && x.ArticleCode == createPurchaseCommand.ArticleCode);

            //var price = maxPrice ?? product.Prices.FirstOrDefault();
            var unitPrice = 0;

            if (createPurchaseCommand.ArticleCode != null)
            {
                var holooPrice = await _articleRepository.GetHolooPrice(createPurchaseCommand.ArticleCode,
                    (Price.HolooSellNumber) createPurchaseCommand.SellNumber);
                if (holooPrice.exist - createPurchaseCommand.Quantity <= 0)
                    return Ok(new ApiResult
                    {
                        Code = ResultCode.NotExist,
                        Messages = new[] {"کالا موجود نمی باشد"}
                    });
                unitPrice = holooPrice.price;
            }
            else
            {
                var price = product?.Prices?.FirstOrDefault(prs =>
                    prs.IsColleague == createPurchaseCommand.IsColleague &&
                    prs.MinQuantity <= createPurchaseCommand.Quantity &&
                    prs.MaxQuantity >= createPurchaseCommand.Quantity);
                unitPrice = price?.Amount ?? 0;
            }

            var sumPrice = unitPrice * createPurchaseCommand.Quantity;
            var repetitivePurchaseOrder =
                await _purchaseOrderRepository.GetByUser(createPurchaseCommand.UserId, cancellationToken);
            if (repetitivePurchaseOrder != null)
            {
                var repetitiveDetail =
                    repetitivePurchaseOrder.PurchaseOrderDetails.FirstOrDefault(x =>
                        x.ProductId == createPurchaseCommand.ProductId);
                if (repetitiveDetail != null)
                {
                    repetitiveDetail.Quantity += createPurchaseCommand.Quantity;
                    repetitiveDetail.SumPrice = repetitiveDetail.Quantity * unitPrice;
                    await _purchaseOrderDetailRepository.UpdateAsync(repetitiveDetail, cancellationToken);
                }
                else
                {
                    await _purchaseOrderDetailRepository.AddAsync(new PurchaseOrderDetail
                    {
                        PurchaseOrderId = repetitivePurchaseOrder.Id,
                        Name = product.Name,
                        UnitPrice = unitPrice,
                        ProductId = product.Id,
                        Quantity = createPurchaseCommand.Quantity,
                        SumPrice = sumPrice
                    }, cancellationToken);
                    repetitivePurchaseOrder.Amount += sumPrice;
                    await _purchaseOrderRepository.UpdateAsync(repetitivePurchaseOrder, cancellationToken);
                }

                return Ok(repetitivePurchaseOrder);
            }

            purchaseOrder = await _purchaseOrderRepository.AddAsync(new PurchaseOrder
            {
                Amount = sumPrice,
                Status = 0,
                UserId = createPurchaseCommand.UserId
            }, cancellationToken);
            purchaseOrderDetail = await _purchaseOrderDetailRepository.AddAsync(new PurchaseOrderDetail
            {
                PurchaseOrderId = purchaseOrder.Id,
                Name = product.Name,
                UnitPrice = unitPrice,
                ProductId = product.Id,
                Quantity = createPurchaseCommand.Quantity,
                SumPrice = sumPrice
            }, cancellationToken);
            purchaseOrder.PurchaseOrderDetails.Add(purchaseOrderDetail);
            return Ok(purchaseOrder);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult {Code = ResultCode.DatabaseError});
        }
    }

    [HttpPut]
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(PurchaseOrder purchaseOrder, CancellationToken cancellationToken)
    {
        try
        {
            await _purchaseOrderRepository.UpdateAsync(purchaseOrder, cancellationToken);
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
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _purchaseOrderRepository.DeleteAsync(id, cancellationToken);
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