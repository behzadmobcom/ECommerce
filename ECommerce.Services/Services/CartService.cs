using Entities;
using Entities.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Helper;
using Services.IServices;

namespace Services.Services
{
    public class CartService : EntityService<PurchaseOrderViewModel>, ICartService
    {
        private readonly IHttpService _http;

        private const string Url = "api/PurchaseOrders";
        private readonly ICookieService _cookieService;
        private readonly IProductService _productService;
        private readonly string _key= "Cart";


        public CartService(IHttpService http,ICookieService cookieService, IProductService productService) : base(http)
        {
            _http = http;
            _cookieService = cookieService;
            _productService = productService;
        }

        public async Task<ServiceResult<List<PurchaseOrderViewModel>>> Load(HttpContext context)
        {
            
            var currentUser =  _cookieService.GetCurrentUser();
            if (currentUser.Id != 0)
            {
                var result = await ReadList(Url,$"UserCart?userId={currentUser.Id}");
                return Return<List<PurchaseOrderViewModel>>(result);
            }
            
            List<int> productIdList = new List<int>();
            List<int> productNumberList = new List<int>();
            var cookies = _cookieService.GetCookie(context, _key);
            foreach (var cookie in cookies.OrderBy(x=>x.Key))
            {
                var temp = cookie.Key.Split("-");
                productIdList.Add(Convert.ToInt32(temp[1]));
                productNumberList.Add(Convert.ToInt32(cookie.Value));
            }
            var responseProduct = await _productService.ProductsWithIdsForCart(productIdList);
            if (responseProduct.Code > 0) return new ServiceResult<List<PurchaseOrderViewModel>>{Code = ServiceCode.Error,Message = responseProduct.Message};
            var cart = new List<PurchaseOrderViewModel>();
            for (int i = 0; i < responseProduct.ReturnData.Count; i++)
            {
                var tempPurchaseOrderDetail = new PurchaseOrderViewModel
                {
                    ProductId = responseProduct.ReturnData[i].Id,
                    Quantity = productNumberList[i],
                    Name = responseProduct.ReturnData[i].Name,
                    Price = responseProduct.ReturnData[i].Price,
                    Url= responseProduct.ReturnData[i].Url,
                    ImagePath = responseProduct.ReturnData[i].ImagePath,
                    Alt = responseProduct.ReturnData[i].Alt,
                    Brand = responseProduct.ReturnData[i].Brand,
                    SumPrice = responseProduct.ReturnData[i].Price* productNumberList[i]
                };

                cart.Add(tempPurchaseOrderDetail);
            }
            return new ServiceResult<List<PurchaseOrderViewModel>>
            {
                Code = ServiceCode.Success,
                ReturnData = cart
            };
        }

        public async Task<ServiceResult> Add(HttpContext context,int productId)
        {
            var currentUser = _cookieService.GetCurrentUser();
            if (currentUser.Id == 0)
            {
                var count = 1;
                var product = _cookieService.GetCookie(context, $"{_key}-{productId}",false);
                if (product != null) count = product.FirstOrDefault().Value + 1;

                _cookieService.SetCookie(context, new CookieData($"{_key}-{productId}", count));
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
                Quantity = 1,
                ProductId = productId
            };
            var result = await Create(Url, purchaseOrderViewModel);
            //var result = await _http.PostAsync(Url, purchaseOrderViewModel);
            return Return(result);
        }

        public async Task<ServiceResult> Delete(HttpContext context,int productId)
        {
            var currentUser = _cookieService.GetCurrentUser();
            if ( currentUser.Id == 0)
            {
                var count = 0;
                var product = _cookieService.GetCookie(context, $"{_key}-{productId}", false);
                if (product != null)
                {
                    count = product.FirstOrDefault().Value-1;
                    if (count == 0)
                    {
                        _cookieService.Remove(context, new CookieData($"{_key}-{productId}", productId));
                        return new ServiceResult
                        {
                            Code = ServiceCode.Success,
                            Message = "کالا با موفقیت از سبد شما حذف شد"
                        };
                    }
                    _cookieService.SetCookie(context, new CookieData($"{_key}-{productId}", count));
                    return new ServiceResult
                    {
                        Code = ServiceCode.Success
                    };
                }
            }
            var success = await Delete(Url, productId);
            if(success.Code==ResultCode.Success)  return new ServiceResult{Code = ServiceCode.Success };
            return new ServiceResult { Code = ServiceCode.Error };
        }

        public async Task<int> Count(HttpContext context)
        {
            var cartList = await Load(context);
            return cartList.ReturnData.Count;
        }
    }
}
