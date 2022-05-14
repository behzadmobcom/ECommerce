using Entities;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.Products;

public class DetailModel : PageModel
{
    private readonly IBrandService _brandService;
    private readonly ICategoryService _categoryService;
    private readonly IDiscountService _discountService;
    private readonly IHostEnvironment _environment;
    private readonly IImageService _imageService;
    private readonly IKeywordService _keywordService;
    private readonly IProductService _productService;
    private readonly IStoreService _storeService;
    private readonly ISupplierService _supplierService;
    private readonly ITagService _tagService;

    public DetailModel(IProductService productService, ITagService tagService, ICategoryService categoryService,
        IHostEnvironment environment,
        IKeywordService keywordService, IBrandService brandService, IDiscountService discountService,
        IStoreService storeService,
        ISupplierService supplierService, IImageService imageService)
    {
        _productService = productService;
        _tagService = tagService;
        _categoryService = categoryService;
        _environment = environment;
        _keywordService = keywordService;
        _brandService = brandService;
        _discountService = discountService;
        _storeService = storeService;
        _supplierService = supplierService;
        _imageService = imageService;
    }

    public Dictionary<int, string> Discounts { get; set; }
    public Dictionary<int, string> Stores { get; set; }
    public Dictionary<int, string> Suppliers { get; set; }
    public Dictionary<int, string> Brands { get; set; }
    public Dictionary<int, string> Tags { get; set; }
    public Dictionary<int, string> Keywords { get; set; }
    public List<TreeViewModel> CategoriesTreeViewModels { get; set; }
    [BindProperty] public Product Product { get; set; }
    [BindProperty] public IFormFile Upload { get; set; }

    //public async Task<IActionResult> OnGet(int id)
    //{
    //    //var result = await _productService.GetById(id);
    //    //if (result.Code == 0)
    //    //{
    //    //    Product = result.ReturnData;
    //    //    return Page();
    //    //}

    //    //return RedirectToPage("/Products/Index",
    //    //    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
    //}
}