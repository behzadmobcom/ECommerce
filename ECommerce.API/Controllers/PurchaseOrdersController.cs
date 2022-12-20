using ECommerce.API.Interface;
using ECommerce.Application.Commands.Purchase.Purchases;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.HolooEntity;
using Ecommerce.Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PurchaseOrdersController : ControllerBase
{
    private readonly IHolooArticleRepository _articleRepository;
    private readonly ILogger<PurchaseOrdersController> _logger;
    private readonly IProductRepository _productRepository;
    private readonly IPriceRepository _priceRepository;
    private readonly IPurchaseOrderDetailRepository _purchaseOrderDetailRepository;
    private readonly IPurchaseOrderRepository _purchaseOrderRepository;
    private readonly IHolooABailRepository _holooABailRepository;
    private readonly IHolooFBailRepository _holooFBailRepository;
    private readonly IHolooSanadRepository _holooSanadRepository;
    private readonly IHolooSanadListRepository _holooSanadListRepository;
    private readonly IHolooCustomerRepository _holooCustomerRepository;
    private readonly IUserRepository _userRepository;


    public PurchaseOrdersController(IPurchaseOrderRepository discountRepository,
        IPurchaseOrderDetailRepository purchaseOrderDetailRepository, IProductRepository productRepository,
        ILogger<PurchaseOrdersController> logger, IHolooArticleRepository articleRepository, IPriceRepository priceRepository, IHolooFBailRepository holooFBailRepository, IHolooABailRepository holooABailRepository, IHolooSanadRepository holooSanadRepository, IHolooSanadListRepository holooSanadListRepository, IUserRepository userRepository, IHolooCustomerRepository holooCustomerRepository)
    {
        _purchaseOrderRepository = discountRepository;
        _purchaseOrderDetailRepository = purchaseOrderDetailRepository;
        _productRepository = productRepository;
        _logger = logger;
        _articleRepository = articleRepository;
        _priceRepository = priceRepository;
        _holooFBailRepository = holooFBailRepository;
        _holooABailRepository = holooABailRepository;
        _holooSanadRepository = holooSanadRepository;
        _holooSanadListRepository = holooSanadListRepository;
        _userRepository = userRepository;
        _holooCustomerRepository = holooCustomerRepository;
    }
    private async Task<List<PurchaseOrderViewModel>> AddPriceAndExistFromHolooList(
        List<PurchaseOrderViewModel> products)
    {
        foreach (var product in products.Where(x => x.Price.ArticleCode != null))
            if (product.Price.SellNumber != null && product.Price.SellNumber != Price.HolooSellNumber.خالی)
            {
                var article = await _articleRepository.GetHolooPrice(product.Price.ArticleCodeCustomer!,
                    product.Price.SellNumber!.Value);
                product.PriceAmount = article.price / 10;
                product.Exist = (double)article.exist;
                product.SumPrice = product.PriceAmount * product.Quantity;
            }

        return products;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PurchaseFiltreOrderViewModel purchaseFiltreOrderViewModel,
       CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(purchaseFiltreOrderViewModel.PaginationParameters.Search)) purchaseFiltreOrderViewModel.PaginationParameters.Search = "";
            var entity = await _purchaseOrderRepository.Search(purchaseFiltreOrderViewModel, cancellationToken);
            var paginationDetails = new PaginationDetails
            {
                TotalCount = entity.TotalCount,
                PageSize = entity.PageSize,
                CurrentPage = entity.CurrentPage,
                TotalPages = entity.TotalPages,
                HasNext = entity.HasNext,
                HasPrevious = entity.HasPrevious,
                Search = purchaseFiltreOrderViewModel.PaginationParameters.Search
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
            return Ok(new ApiResult { Code = ResultCode.DatabaseError });
        }
    }

    [HttpGet]
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<ActionResult<PurchaseOrder>> GetByUserId(int userId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _purchaseOrderRepository.GetByUser(userId, cancellationToken);
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
            return Ok(new ApiResult { Code = ResultCode.DatabaseError });
        }
    }

    [HttpGet]
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<ActionResult<PurchaseOrder>> GetByOrderId(long orderId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _purchaseOrderRepository.GetByOrderId(orderId, cancellationToken);
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
            return Ok(new ApiResult { Code = ResultCode.DatabaseError });
        }
    }

    [HttpGet]
    //[Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<ActionResult<PurchaseOrder>> GetByOrderIdWithInclude(long orderId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _purchaseOrderRepository.GetByOrderIdWithInclude(orderId, cancellationToken);
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
            return Ok(new ApiResult { Code = ResultCode.DatabaseError });
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

            if (result.Any(x => x.Price.ArticleCode != null))
                result = await AddPriceAndExistFromHolooList(result.ToList());
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = result
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult { Code = ResultCode.DatabaseError });
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
            var product = await _productRepository.GetByIdAsync(cancellationToken, createPurchaseCommand.ProductId);
            var prices = await _priceRepository.PriceOfProduct(createPurchaseCommand.ProductId, cancellationToken);
            var price = prices.FirstOrDefault(x => x.Id == createPurchaseCommand.PriceId);

            //var colleaguePrice = product.Prices.Where(x => x.IsColleague == createPurchaseCommand.IsColleague ).ToList();
            //var minPrice = colleaguePrice.Any()
            //    ? colleaguePrice.Where(x => x.MinQuantity <= createPurchaseCommand.Quantity).ToList()
            //    : product.Prices.Where(x => x.MinQuantity <= createPurchaseCommand.Quantity && x.ArticleCode == createPurchaseCommand.ArticleCode).ToList();
            //var maxPrice = minPrice.Any() ? minPrice.FirstOrDefault(x => x.MaxQuantity >= createPurchaseCommand.Quantity) :
            //    product.Prices.FirstOrDefault(x => x.MaxQuantity >= createPurchaseCommand.Quantity && x.ArticleCode == createPurchaseCommand.ArticleCode);

            //var price = maxPrice ?? product.Prices.FirstOrDefault();
            decimal unitPrice = 0;
            var repetitivePurchaseOrder =
                await _purchaseOrderRepository.GetByUser(createPurchaseCommand.UserId, cancellationToken);

            var repetitivePurchaseOrderDetails =
                repetitivePurchaseOrder?.PurchaseOrderDetails?.FirstOrDefault(x =>
                    x.ProductId == createPurchaseCommand.ProductId);

            var repetitiveQuantity = repetitivePurchaseOrderDetails?.Quantity ?? 0;

            if (createPurchaseCommand.Quantity + repetitiveQuantity > product.MaxOrder)
            {
                return Ok(new ApiResult
                {
                    Code = ResultCode.NotExist,
                    Messages = new[] { $"تعداد انتخابی کالا بیشتر از حد مجاز است. حد مجاز {product.MaxOrder} است" }
                });
            }

            if (product.MinInStore == null) product.MinInStore = 0;

            if (price.ArticleCode != null)
            {
                var holooPrice = await _articleRepository.GetHolooPrice(price.ArticleCodeCustomer,
                    (Price.HolooSellNumber)price.SellNumber);
                if (repetitiveQuantity + createPurchaseCommand.Quantity > holooPrice.exist + product.MinInStore)
                    return Ok(new ApiResult
                    {
                        Code = ResultCode.NotExist,
                        Messages = new[] { "تعداد انتخابی کالا بیشتر از موجودی است" }
                    });
                unitPrice = holooPrice.price;
            }
            else
            {

                if (repetitiveQuantity+ createPurchaseCommand.Quantity > price.Exist + product.MinInStore)
                    return Ok(new ApiResult
                    {
                        Code = ResultCode.NotExist,
                        Messages = new[] { "تعداد انتخابی کالا بیشتر از موجودی است" }
                    });
                unitPrice = price?.Amount ?? 0;
            }

            decimal sumPrice = unitPrice * createPurchaseCommand.Quantity;
           
            if (repetitivePurchaseOrder != null)
            {
                var repetitiveDetail =
                    repetitivePurchaseOrder.PurchaseOrderDetails.FirstOrDefault(x =>
                        x.ProductId == createPurchaseCommand.ProductId && x.PriceId == createPurchaseCommand.PriceId);
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
                        PriceId = price!.Id,
                        Quantity = createPurchaseCommand.Quantity,
                        SumPrice = sumPrice
                    }, cancellationToken);
                    repetitivePurchaseOrder.Amount += sumPrice;
                    await _purchaseOrderRepository.UpdateAsync(repetitivePurchaseOrder, cancellationToken);
                }

                return Ok(new ApiResult()
                {
                    Messages = new[] { "کالا با موفقیت به سبد خرید اضافه شد" },
                    Code = ResultCode.Success

                });
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
                PriceId = price!.Id,
                Quantity = createPurchaseCommand.Quantity,
                SumPrice = sumPrice
            }, cancellationToken);
            purchaseOrder.PurchaseOrderDetails.Add(purchaseOrderDetail);
            return Ok(new ApiResult()
            {
                Messages = new[] { "کالا با موفقیت به سبد خرید اضافه شد" },
                Code = ResultCode.Success

            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult { Code = ResultCode.DatabaseError });
        }
    }

    [HttpPut]
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Decrease(PurchaseOrder purchaseOrder, CancellationToken cancellationToken)
    {
        try
        {
            var purchaseOrderDetails = await _purchaseOrderDetailRepository.GetByIdAsync(cancellationToken, purchaseOrder.Id);
            purchaseOrderDetails.Quantity -= 1;
            if (purchaseOrderDetails.Quantity <= 0)
            {
                await _purchaseOrderDetailRepository.DeleteAsync(purchaseOrderDetails.Id, cancellationToken);
            }
            else
            {
                await _purchaseOrderDetailRepository.UpdateAsync(purchaseOrderDetails, cancellationToken);
            }
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult { Code = ResultCode.DatabaseError });
        }
    }

    [HttpPut]
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Pay(PurchaseOrder purchaseOrder, CancellationToken cancellationToken)
    {
        try
        {
            if (purchaseOrder == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });
            if (string.IsNullOrEmpty(purchaseOrder.Description)) purchaseOrder.Description = "";
            purchaseOrder.PaymentDate = DateTime.Now;
            purchaseOrder.Transaction.TransactionDate = DateTime.Now;
            purchaseOrder.Transaction.PurchaseOrders = new List<PurchaseOrder>();
            purchaseOrder.Transaction.PurchaseOrders.Add(purchaseOrder);
            var resultUser =await _userRepository.GetByIdAsync(cancellationToken, purchaseOrder.UserId);
            var cCode = resultUser.CustomerCode;
            var (fCode, fCodeC) = await _holooFBailRepository.GetFactorCode(cancellationToken);
            var fBail = await _holooFBailRepository.Add(new HolooFBail
            {
                C_Code = cCode,
                Fac_Code = fCode,
                Fac_Code_C = fCodeC,
                Fac_Comment = $"پیش فاکتور از سایت برای سفارش شماره {purchaseOrder.OrderGuid} به آدرس : {purchaseOrder.SendInformation.State.Name} - {purchaseOrder.SendInformation.City.Name} - {purchaseOrder.SendInformation.Address}, کد پستی : {purchaseOrder.SendInformation.PostalCode}, شماره تماس : {purchaseOrder.SendInformation.Mobile}",
                Fac_Date = DateTime.Now,
                Fac_Time = DateTime.Now,
                Fac_Type = "P",
                Sum_Price =Convert.ToDouble(purchaseOrder.Amount)

            }, cancellationToken);

            var aBail = new List<HolooABail>();
            var i = 1;

            var purchaseOrderDetails = await _purchaseOrderDetailRepository.GetByPurchaseOrderId(purchaseOrder.Id, cancellationToken);
            foreach (var orderDetail in purchaseOrderDetails)
            {

                aBail.Add(new HolooABail
                {
                    A_Code = orderDetail.Price.ArticleCode,
                    ACode_C = orderDetail.Price.ArticleCodeCustomer,
                    A_Index = Convert.ToInt16(i++),
                    DarsadTakhfif = Convert.ToDouble(orderDetail.DiscountAmount),
                    Fac_Code = fBail,
                    Fac_Type = "P",
                    Few_Article = orderDetail.Quantity,
                    First_Article = orderDetail.Quantity,
                    Price_BS =Convert.ToDouble( orderDetail.UnitPrice),
                    Unit_Few = 0
                });
            }
            await _holooABailRepository.Add(aBail, cancellationToken);
            purchaseOrder.FBailCode = fBail;


            var customer =await _holooCustomerRepository.GetCustomerByCode(cCode);
            var sanad = new HolooSanad(purchaseOrder.Description);
            var sanadCode = Convert.ToInt32(await _holooSanadRepository.Add(sanad, cancellationToken));
            purchaseOrder.Transaction.SanadCode = sanadCode;

            await _holooSanadListRepository.Add(new HolooSndList(sanadCode, "102", "0001", "0001", Convert.ToDouble(purchaseOrder.Amount),0, $"فاکتور شماره {fCodeC} سفارش در سایت به شماره {purchaseOrder.OrderGuid}"), cancellationToken);
            await _holooSanadListRepository.Add(new HolooSndList(sanadCode, "103", customer.Moien_Code_Bed, "", 0,Convert.ToDouble(purchaseOrder.Amount), $"فاکتور شماره {fCodeC} سفارش در سایت به شماره {purchaseOrder.OrderGuid}"), cancellationToken);

            purchaseOrder.IsPaid = true;

            await _purchaseOrderRepository.UpdateAsync(purchaseOrder, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult { Code = ResultCode.DatabaseError });
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
            return Ok(new ApiResult { Code = ResultCode.DatabaseError });
        }
    }

    [HttpDelete]
    [Authorize(Roles = "Client,Admin,SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _purchaseOrderDetailRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult { Code = ResultCode.DatabaseError });
        }
    }


}