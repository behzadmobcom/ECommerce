using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Http;
using ECommerce.Services.IServices;

namespace Services.Services;

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

    public ServiceResult Add(HttpContext context, int productId)
    {
        var cookies = Load(context).ReturnData;
        if (cookies.Count >= 4)
            return new ServiceResult
            {
                Code = ServiceCode.Warning,
                Message = "مقایسه بیشتر از 4 کالا امکانپذیر نمی باشد"
            };
        _cookieService.SetCookie(context, new CookieData($"{_key}-{productId}", productId));
        return new ServiceResult {Code = ServiceCode.Success};
    }

    public ServiceResult Remove(HttpContext context, int productId)
    {
        _cookieService.Remove(context, new CookieData(_key, productId));
        return new ServiceResult {Code = ServiceCode.Success};
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

    public Task<ServiceResult<List<ProductCompareViewModel>>> CompareList(HttpContext context)
    {
        var cookies = Load(context).ReturnData;
        var result = _productService.ProductsWithIdsForCompare(cookies);
        return result;
    }
}