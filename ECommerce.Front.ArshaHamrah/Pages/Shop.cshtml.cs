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
    public int Sort { get; set; }
    public int Min { get; set; }
    public int Max { get; set; }
  [BindProperty]  public bool IsExist { get; set; }
    //public PaginationViewModel Pagination { get; set; }
    public ServiceResult<List<ProductIndexPageViewModel>> Products { get; set; }
    public List<ProductIndexPageViewModel> NewProducts { get; set; }

    public async Task OnGet(string? path = null, string? search = null, int pageNumber = 1, int pageSize = 9, int productSort = 1,
        string? message = null, string? code = null,int minprice= 0, int maxprice = 0, bool isExist = false)
    {
        IsExist = isExist;
        Min = 100000;
        Max = 200000000;
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
        Products = await _productService.TopProducts(categoryId, search, pageNumber, pageSize, productSort, maxprice, minprice, IsExist);
       
        await Initial();

        Sort = productSort;
    }

    private async Task Initial()
    {
        NewProducts = (await _productService.TopNewShop()).ReturnData;
        var result = _cookieService.GetCurrentUser();
        if (result.Id > 0) IsColleague = result.IsColleague;
        IsColleague = false;
        var categoryResult = await _categoryService.GetParents();
        Categories = categoryResult.ReturnData;
    }

    public async Task<IActionResult> OnPostPriceRange(string? path = null,int minprice=0, int maxprice=0, bool isExist=false)
    {
        IsExist = isExist;
        string categoryId = "0";
        if (!string.IsNullOrEmpty(path))
        {
            var resultCategory = await _categoryService.GetByUrl(path);
            if (resultCategory.Code == ServiceCode.Success) categoryId = resultCategory.ReturnData.Id.ToString();
        }
        Products = await _productService.TopProducts(categoryId, "", 0, 9, 1, maxprice, minprice, IsExist);
        await Initial();
        Max = maxprice;
        Min = minprice;
        return Page();
    }

}