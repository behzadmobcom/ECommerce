﻿using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Services.Services;

public class CartService : EntityService<PurchaseOrderViewModel>, ICartService
{
    private const string Url = "api/PurchaseOrders";
    private readonly ICookieService _cookieService;
    private readonly IHttpService _http;
    private readonly string _key = "Cart";
    private readonly IProductService _productService;
    private readonly IPriceService _priceService;


    public CartService(IHttpService http, ICookieService cookieService, IProductService productService, IPriceService priceService) : base(http)
    {
        _http = http;
        _cookieService = cookieService;
        _productService = productService;
        _priceService = priceService;
    }

    public async Task<ServiceResult<List<PurchaseOrderViewModel>>> Load(HttpContext context)
    {
        var currentUser = _cookieService.GetCurrentUser();
        var carts = await ReadFromCookies(context);

        if (currentUser.Id != 0)
        {
            if (carts.Count > 0)
            {
                foreach (var cart in carts)
                {
                    await Add(context, cart.ProductId, cart.PriceId, cart.Quantity);
                    await Delete(context, cart.Id, cart.ProductId, cart.PriceId, true);
                }
            }

            var result = await ReadList(Url, $"UserCart?userId={currentUser.Id}");
            return Return(result);
        }

        //if(cart.Count==0)
        //    return new ServiceResult<List<PurchaseOrderViewModel>>
        //    { Code = ServiceCode.Error, Message = "کالای مورد نظر یافت نشد"};

        return new ServiceResult<List<PurchaseOrderViewModel>>
        {
            Code = ServiceCode.Success,
            ReturnData = carts
        };
    }

    private struct CookiesProduct
    {
        public int ProductId { get; set; }
        public ushort ProductNumber { get; set; }
        public int ProductPrice { get; set; }
    }

    private async Task<List<PurchaseOrderViewModel>> ReadFromCookies(HttpContext context)
    {
        var carts = new List<PurchaseOrderViewModel>();

        var productList = new List<CookiesProduct>();

        var cookies = _cookieService.GetCookie(context, _key);
        foreach (var cookie in cookies.OrderBy(x => x.Key))
        {
            var temp = cookie.Key.Split("-");
            var productCode = Convert.ToInt32(temp[1]);
            var productCount = Convert.ToUInt16(cookie.Value);
            var priceId = Convert.ToInt32(temp[2]);
            if (productCode <= 0 || productCount <= 0 || priceId <= 0) continue;
            productList.Add(new CookiesProduct
            {
                ProductId = productCode,
                ProductNumber = productCount,
                ProductPrice = priceId,
            });
        }

        var responseProduct = await _productService.ProductsWithIdsForCart(productList.Select(x => x.ProductId).ToList());
        if (responseProduct.Code > 0)
            return carts;

        for (var i = 0; i < responseProduct.ReturnData.Count; i++)
        {
            var product = productList.First(x => x.ProductId == responseProduct.ReturnData[i].Id);
            var priceId = product.ProductPrice;
            var price = responseProduct.ReturnData[i].Prices.Where(x => x.Id == priceId).FirstOrDefault();
            if (price == null)
                continue;
            var quantity = responseProduct.ReturnData[i].MaxOrder < product.ProductNumber && responseProduct.ReturnData[i].MaxOrder > 0
                ? responseProduct.ReturnData[i].MaxOrder
                : product.ProductNumber;
            var tempPurchaseOrderDetail = new PurchaseOrderViewModel
            {
                ProductId = responseProduct.ReturnData[i].Id,
                Quantity = quantity,
                Name = responseProduct.ReturnData[i].Name,
                Price = price,
                Url = responseProduct.ReturnData[i].Url,
                ImagePath = responseProduct.ReturnData[i].ImagePath,
                Alt = responseProduct.ReturnData[i].Alt,
                Brand = responseProduct.ReturnData[i].Brand,
                SumPrice = price.Amount * quantity,
                PriceAmount = price.Amount,
                PriceId = priceId,
                ColorName = price.Color.Name
            };

            carts.Add(tempPurchaseOrderDetail);
        }

        return carts;
    }

    public async Task<ServiceResult<List<PurchaseOrderViewModel>>> CartListFromServer()
    {
        var currentUser = _cookieService.GetCurrentUser();
        if (currentUser.Id != 0)
        {
            var result = await ReadList(Url, $"UserCart?userId={currentUser.Id}");
            return Return(result);
        }
        return new ServiceResult<List<PurchaseOrderViewModel>>
        {
            Code = ServiceCode.Error
        };
    }

    public async Task<ServiceResult> Add(HttpContext context, int productId, int priceId, int count)
    {
        var productResult = await _productService.ProductsWithIdsForCart(new List<int> { productId });
        var productFromServer = productResult.ReturnData[0];

        var exist = productFromServer.Prices.First(x => x.Id == priceId).Exist;
        var maxOrder = productFromServer.MaxOrder;

        var currentUser = _cookieService.GetCurrentUser();

        if (currentUser.Id == 0)
        {
            var product = _cookieService.GetCookie(context, $"{_key}-{productId}-{priceId}", false);
            var newCount = product.FirstOrDefault()!.Value + count;

            if (newCount > exist)
            {
                return new ServiceResult
                {
                    Code = ServiceCode.Error,
                    Message = "تعداد کالا بیشتر از موجودی است"
                };
            }

            if (newCount > maxOrder && maxOrder > 0)
            {
                return new ServiceResult
                {
                    Code = ServiceCode.Error,
                    Message = "تعداد کالا بیشتر از حد مجاز است"
                };
            }

            _cookieService.SetCookie(context, new CookieData($"{_key}-{productId}-{priceId}", newCount));
            if (newCount > 1)
            {
                return new ServiceResult
                {
                    Code = ServiceCode.Info,
                    Message = "کالا با موفقیت به سبد خرید اضافه شد"
                };
            }
            return new ServiceResult
            {
                Code = ServiceCode.Success,
                Message = "کالا با موفقیت به سبد خرید اضافه شد"
            };
        }

        var purchaseOrderViewModel = new PurchaseOrderViewModel
        {
            IsColleague = currentUser.IsColleague,
            UserId = currentUser.Id,
            Quantity = Convert.ToUInt16(count),
            ProductId = productId,
            PriceId = priceId
        };
        var result = await Create(Url, purchaseOrderViewModel);
        if (result.Code == ResultCode.Repetitive)
        {
            return new ServiceResult
            {
                Code = ServiceCode.Info,
               Message = result.Messages?.FirstOrDefault()
            };
        }
        return Return(result);
    }

    public async Task<ServiceResult> Decrease(HttpContext context, int id, int productId, int priceId)
    {
        var currentUser = _cookieService.GetCurrentUser();
        if (currentUser.Id == 0)
        {
            var product = _cookieService.GetCookie(context, $"{_key}-{productId}-{priceId}", false);
            if (product.Any())
            {
                var count = product.FirstOrDefault()!.Value - 1;
                if (count <= 0)
                {
                    _cookieService.Remove(context, new CookieData($"{_key}-{productId}-{priceId}", productId));
                    return new ServiceResult
                    {
                        Code = ServiceCode.Success,
                        Message = "کالا با موفقیت از سبد شما حذف شد"
                    };
                }

                _cookieService.SetCookie(context, new CookieData($"{_key}-{productId}-{priceId}", count));
                return new ServiceResult
                {
                    Code = ServiceCode.Success,
                    Message = "کالا با موفقیت از سبد شما کم شد"
                };
            }
        }

        var success = await Update(Url, new PurchaseOrderViewModel { Id = id }, "Decrease");
        return Return(success);
    }

    public async Task<ServiceResult> Delete(HttpContext context, int id, int productId, int priceId, bool deleteFromCookie = false)
    {
        var currentUser = _cookieService.GetCurrentUser();
        if (currentUser.Id == 0 || deleteFromCookie)
        {
            var product = _cookieService.GetCookie(context, $"{_key}-{productId}-{priceId}", false);
            if (product.Any())
            {
                _cookieService.Remove(context, new CookieData($"{_key}-{productId}-{priceId}", productId));
                return new ServiceResult
                {
                    Code = ServiceCode.Success,
                    Message = "کالا با موفقیت از سبد شما حذف شد"
                };
            }
        }

        var success = await Delete(Url, id);
        return Return(success);
    }


}