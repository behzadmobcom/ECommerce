using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;

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

    public List<CategoryParentViewModel> Categories { get; set; }
    public bool IsColleague { get; set; }
    //public PaginationViewModel Pagination { get; set; }
    public ServiceResult<List<ProductIndexPageViewModel>> Products { get; set; }
    public List<ProductIndexPageViewModel> NewProducts { get; set; }

    public async Task OnGet(string? path = null ,string? search = null, int pageNumber = 1, int pageSize = 9, int productSort = 1,
        string? message = null, string? code = null)
    {
    
        NewProducts = (await _productService.TopNewShop()).ReturnData;
        string categoryId = "0";
        if (!string.IsNullOrEmpty(path))
        {
            var resultCategory = await _categoryService.GetByUrl(path);
            if (resultCategory.Code == ServiceCode.Success) categoryId = resultCategory.ReturnData.Id.ToString();
        }
        if (!string.IsNullOrEmpty(search))
        {
            search = $"Name={search}";
        }
            Products = await _productService.TopProducts(categoryId, search, pageNumber, pageSize, productSort);
        //var brandResult = await _brandService.LoadDictionary();
        //if (brandResult.Code == ServiceCode.Success) Brands = brandResult.ReturnData;
        var result = _cookieService.GetCurrentUser();

        if (result.Id > 0) IsColleague = result.IsColleague;
        IsColleague = false;

        var categoryResult = await _categoryService.GetParents();
        Categories = categoryResult.ReturnData;
    }

}