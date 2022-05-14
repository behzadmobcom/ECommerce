using Entities;
using Entities.Helper;
using Entities.HolooEntity;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.Products;

public class CreateModel : PageModel
{
    private readonly IBrandService _brandService;
    private readonly ICategoryService _categoryService;
    private readonly IDiscountService _discountService;
    private readonly IHostEnvironment _environment;
    private readonly IHolooArticleService _holooArticleService;
    private readonly IHolooMGroupService _holooMGroupService;
    private readonly IHolooSGroupService _holooSGroupService;
    private readonly IImageService _imageService;
    private readonly IKeywordService _keywordService;
    private readonly IProductService _productService;
    private readonly IStoreService _storeService;
    private readonly ISupplierService _supplierService;
    private readonly ITagService _tagService;

    public CreateModel(IProductService productService, ITagService tagService, ICategoryService categoryService,
        IHostEnvironment environment,
        IKeywordService keywordService, IBrandService brandService, IDiscountService discountService,
        IStoreService storeService,
        ISupplierService supplierService, IImageService imageService, IHolooMGroupService holooMGroupService,
        IHolooSGroupService holooSGroupService,
        IHolooArticleService holooArticleService)
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
        _holooMGroupService = holooMGroupService;
        _holooSGroupService = holooSGroupService;
        _holooArticleService = holooArticleService;
    }

    public SelectList Discounts { get; set; }
    public SelectList Stores { get; set; }
    public SelectList Suppliers { get; set; }
    public SelectList Brands { get; set; }
    public SelectList Tags { get; set; }
    public SelectList Keywords { get; set; }

    public List<CategoryParentViewModel> CategoryParentViewModel { get; set; }
    public List<HolooMGroup> HolooMGroups { get; set; } = new();
    public List<HolooSGroup> HolooSGroups { get; set; } = new();
    public List<Product> HolooArticle { get; set; } = new();

    [BindProperty] public ProductViewModel Product { get; set; }
    [BindProperty] public IFormFile Upload { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }


    private async Task Initial()
    {
        var stores = (await _storeService.Load()).ReturnData;
        Stores = new SelectList(stores, nameof(Store.Id), nameof(Store.Name));

        var discounts = (await _discountService.Load()).ReturnData;
        Discounts = new SelectList(discounts, nameof(Discount.Id), nameof(Discount.Name));

        var suppliers = (await _supplierService.Load()).ReturnData;
        Suppliers = new SelectList(suppliers, nameof(Supplier.Id), nameof(Supplier.Name));

        var brands = (await _brandService.Load()).ReturnData;
        Brands = new SelectList(brands, nameof(Brand.Id), nameof(Brand.Name));

        var tags = (await _tagService.Load()).ReturnData;
        Tags = new SelectList(tags, nameof(Tag.Id), nameof(Tag.TagText));

        var keywords = (await _keywordService.Load()).ReturnData;
        Keywords = new SelectList(keywords, nameof(Keyword.Id), nameof(Keyword.KeywordText));


        CategoryParentViewModel = (await _categoryService.GetParents()).ReturnData;
        HolooMGroups.Add(new HolooMGroup {M_groupname = "انتخاب گروه اصلی"});
        HolooMGroups.AddRange((await _holooMGroupService.Load()).ReturnData);
        HolooSGroups.Add(new HolooSGroup {S_groupname = "ابتدا گروه اصلی را انتخاب کنید"});
        HolooArticle.Add(new Product {Name = "ابتدا گروه فرعی را انتخاب کنید"});
        //HolooSGroups.AddRange((await _holooSGroupService.Load(HolooMGroups.First().M_groupcode)).ReturnData);
        //HolooArticle = (await _holooArticleService.Load($"{HolooSGroups.First().M_groupcode}{HolooSGroups.First().S_groupcode}")).ReturnData;
    }

    public async Task OnGet()
    {
        await Initial();
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.Add(Product);
            if (result.Code == 0)
                return RedirectToPage("/Products/Index",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        await Initial();
        return Page();
    }

    public async Task<JsonResult> OnGetReturnSGroup(string mCode)
    {
        var result = await _holooSGroupService.Load(mCode);
        if (result.Code == ServiceCode.Success) return new JsonResult(result.ReturnData);

        return new JsonResult(new List<HolooSGroup>());
    }

    public async Task<JsonResult> OnGetReturnArticle(string code)
    {
        var result = await _holooArticleService.Load(code);
        if (result.Code == ServiceCode.Success) return new JsonResult(result.ReturnData);

        return new JsonResult(new List<Product>());
    }

    public JsonResult OnGetChildrenCategory(int code, List<TreeViewModel> categoriesTreeViewModels)
    {
        var childrenCategory = categoriesTreeViewModels.Where(p => p.ParentId == code);

        return new JsonResult(childrenCategory);
    }
}