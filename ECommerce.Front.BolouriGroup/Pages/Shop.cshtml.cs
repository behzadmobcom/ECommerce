using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
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

    public async Task OnGet(string path, string search, int pageNumber = 1, int pageSize = 20, int productSort = 1,
        string message = null, string code = null, string tagText = "")
    {
        string[]? resultPath = path.Split('=');
        if (resultPath.Length > 0)
        {
            if (resultPath[0].Contains("tag"))
            {
                tagText = resultPath[1];
                path = null;
            }
        }

        string? categoryId = null;
        if (!string.IsNullOrEmpty(path))
        {
            var resultCategory = await _categoryService.GetByUrl(path);
            if (resultCategory.Code == ServiceCode.Success) categoryId = resultCategory.ReturnData.Id.ToString();
        }

        Products = await _productService.TopProducts(categoryId, search, pageNumber, pageSize, productSort, isWithoutBail: true, tagText: tagText);
        var brandResult = await _brandService.LoadDictionary();
        if (brandResult.Code == ServiceCode.Success) Brands = brandResult.ReturnData;

        Tags = await _tagService.GetAllProductTags();

    }



}