using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Pages;

public class ShopModel : PageModel
{
    private readonly IBrandService _brandService;
    private readonly ICartService _cartService;
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    private readonly ITagService _tagService;

    public ShopModel(ICategoryService categoryService, IProductService productService, IBrandService brandService,
        ICartService cartService, ITagService tagService)
    {
        _categoryService = categoryService;
        _productService = productService;
        _brandService = brandService;
        _cartService = cartService;
        _tagService = tagService;
    }

    public ServiceResult<List<ProductIndexPageViewModel>> Products { get; set; }
    public ServiceResult<List<Tag>> Tags { get; set; }
    public Dictionary<int, string> Brands { get; set; }
    [BindProperty] public int Min { get; set; }
    [BindProperty] public int Max { get; set; }
    [BindProperty] public bool IsExist { get; set; }
    [BindProperty] public int ProductSort { get; set; }

    public async Task OnGet(string path, string? search = null, int pageNumber = 1, int pageSize = 20, int productSort = 1,
        string? message = null, string? code = null, string tagText = "", int minprice = 0, int maxprice = 0, bool isExist = false)
    {
        string tempSearch = search;
        ProductSort = productSort;
        string[]? resultPath = path.Split('=');
        IsExist = isExist;
        Min = minprice == 0 ? 100000 : minprice;
        Max = maxprice == 0 ? 200000000 : maxprice;
        if (resultPath.Length > 0)
        {
            if (resultPath[0].Contains("tag"))
            {
                tagText = resultPath[1];
                path = null;
            }
        }

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
        Products = await _productService.TopProducts(categoryId, search, pageNumber, pageSize, productSort, maxprice, minprice, IsExist, isWithoutBail: true, tagText: tagText );
        
        var brandResult = await _brandService.LoadDictionary();
        if (brandResult.Code == ServiceCode.Success) Brands = brandResult.ReturnData;

        Products.PaginationDetails.isExist = isExist;
        Products.PaginationDetails.MinPrice = minprice;
        Products.PaginationDetails.MaxPrice = maxprice;
        Products.PaginationDetails.ProductSort = productSort;
        Products.PaginationDetails.Search = tempSearch;

    }



}