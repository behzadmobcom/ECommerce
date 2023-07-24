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

    public ServiceResult Remove(HttpContext context, int productId)
    {
        _cookieService.Remove(context, new CookieData($"{_key}-{productId}", productId));
        return new ServiceResult {Code = ServiceCode.Success, Message = "کالا از لیست مقایسه حذف شد"};
    }

    //public ServiceResult<List<int>> Load(HttpContext context)
    //{
    //    //var cookies = _cookieService.GetCookie(context, _key);
    //    //return new ServiceResult<List<int>>
    //    //{
    //    //    Code = ServiceCode.Success,
    //    //    ReturnData = cookies.Select(x => x.Value).ToList()
    //    //};
    //}

    public async Task<ServiceResult<List<ProductCompareViewModel>>> CompareList(List<int> productListId)
    {
        var result =await _productService.ProductsWithIdsForCompare(productListId);
        return result;
    }
    public async Task<ServiceResult<List<ProductCompareViewModel>>> GetProductsByCategories(List<int> categoryId)
    {
        var result = await _productService.ProductsWithCategoriesForCompare(categoryId);
        return result;
    }
}