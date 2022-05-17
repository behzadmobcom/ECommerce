using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;

namespace ArshaHamrah.Pages;

public class ShopModel : PageModel
{
    private readonly ICartService _cartService;
    private readonly ICategoryService _categoryService;
    private readonly ICookieService _cookieService;
    private readonly IProductService _productService;


    public ShopModel(IProductService productService, ICookieService cookieService, ICategoryService categoryService,
        ICartService cartService)
    {
        _productService = productService;
        _cookieService = cookieService;
        _categoryService = categoryService;
        _cartService = cartService;
    }

    public List<Category> Categories { get; set; }
    public bool IsColleague { get; set; }
    //public PaginationViewModel Pagination { get; set; }
    public ServiceResult<List<ProductIndexPageViewModel>> Products { get; set; }

    public async Task OnGet(string categoryUrl)
    {
        // Pagination = (await _productService.Search(categoryUrl, 1)).ReturnData;
        var resultSearch = await _productService.Search(categoryUrl, 1, 10);
        if (resultSearch.Code == ServiceCode.Success)
        {
            Products = resultSearch;
        }

        var result = _cookieService.GetCurrentUser();
        if (result.Id > 0) IsColleague = result.IsColleague;
        IsColleague = false;

        var categoryResult = await _categoryService.Load();
        Categories = categoryResult.ReturnData;
    }

    public async Task<JsonResult> OnGetAddCart(int id, int priceId)
    {
        var result = await _cartService.Add(HttpContext, id,  priceId);
        return new JsonResult(result);
    }

    public async Task<JsonResult> OnGetLoadCart(int id)
    {
        var result = await _cartService.Load(HttpContext);
        return new JsonResult(result);
    }

    public async Task<JsonResult> OnGetDeleteCart(int id,int productId, int priceId)
    {
        var result = await _cartService.Delete(HttpContext, id, productId, priceId);
        return new JsonResult(result);
    }
}