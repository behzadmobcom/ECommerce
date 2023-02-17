using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Services.Services;

public class CompareService : ICompareService
{
    private readonly ICookieService _cookieService;
    private readonly string _key = "Compare";
    private readonly IProductService _productService;

    public CompareService(ICookieService cookieService, IProductService productService)
    {
        _cookieService = cookieService;
        _productService = productService;
    }

    public ServiceResult<int> Add(HttpContext context, int productId)
    {
        var cookies = Load(context).ReturnData;
        if (cookies.Count >= 4)
            return new ServiceResult<int>
            {
                ReturnData = cookies.Count,
                Code = ServiceCode.Warning,
                Message = "مقایسه بیشتر از 4 کالا امکانپذیر نمی باشد"
            };
        var product = _cookieService.GetCookie(context, $"{_key}-{productId}");
        if (product.Any())
        {
            return new ServiceResult<int>
            {
                ReturnData = cookies.Count,
                Code = ServiceCode.Success,
                Message = "این کالا قبلا برای مقایسه اضافه شده است"
            };
        }
        _cookieService.SetCookie(context, new CookieData($"{_key}-{productId}", productId));
        return new ServiceResult<int> {
            ReturnData = cookies.Count+1,
            Code = ServiceCode.Success,
            Message = "با موفقیت به صفحه مقایسه افزوده شد"
        };
    }

    public ServiceResult Remove(HttpContext context, int productId)
    {
        _cookieService.Remove(context, new CookieData($"{_key}-{productId}", productId));
        return new ServiceResult {Code = ServiceCode.Success, Message = "کالا از لیست مقایسه حذف شد"};
    }

    public ServiceResult<List<int>> Load(HttpContext context)
    {
        var cookies = _cookieService.GetCookie(context, _key);
        return new ServiceResult<List<int>>
        {
            Code = ServiceCode.Success,
            ReturnData = cookies.Select(x => x.Value).ToList()
        };
    }

    public async Task<ServiceResult<List<ProductCompareViewModel>>> CompareList(HttpContext context)
    {
        var cookies = Load(context).ReturnData;
        var result =await _productService.ProductsWithIdsForCompare(cookies);
        return result;
    }
}