using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.Products;

public class CreateModel : PageModel
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


    public CreateModel(IProductService productService, ITagService tagService, ICategoryService categoryService,
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

    public SelectList Discounts { get; set; }
    public SelectList Stores { get; set; }
    public SelectList Suppliers { get; set; }
    public SelectList Brands { get; set; }
    public SelectList Tags { get; set; }
    public SelectList Keywords { get; set; }

    public List<CategoryParentViewModel> CategoryParentViewModel { get; set; }

    [BindProperty] public ProductViewModel Product { get; set; }
    [BindProperty] public List<IFormFile> Uploads { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }


    private async Task Initial()
    {
        //Product = new ProductViewModel();
        var stores = (await _storeService.Load()).ReturnData;
        Stores = new SelectList(stores, nameof(Store.Id), nameof(Store.Name));

        var discounts = (await _discountService.Load()).ReturnData;
        Discounts = new SelectList(discounts, nameof(Discount.Id), nameof(Discount.Name));

        var suppliers = (await _supplierService.Load()).ReturnData;
        Suppliers = new SelectList(suppliers, nameof(Supplier.Id), nameof(Supplier.Name));

        var brands = (await _brandService.Load()).ReturnData;
        Brands = new SelectList(brands, nameof(Brand.Id), nameof(Brand.Name));

        var tags = (await _tagService.GetAll()).ReturnData;
        Tags = new SelectList(tags, nameof(Tag.Id), nameof(Tag.TagText));

        var keywords = (await _keywordService.GetAll()).ReturnData;
        Keywords = new SelectList(keywords, nameof(Keyword.Id), nameof(Keyword.KeywordText));

        CategoryParentViewModel = (await _categoryService.GetParents()).ReturnData;

        //HolooSGroups.AddRange((await _holooSGroupService.Load(HolooMGroups.First().M_groupcode)).ReturnData);
        //HolooArticle = (await _holooArticleService.Load($"{HolooSGroups.First().M_groupcode}{HolooSGroups.First().S_groupcode}")).ReturnData;
    }

    public async Task OnGet()
    {
        await Initial();
    }

    public async Task<IActionResult> OnPost()
    {
        if (Uploads == null)
        {
            Message = "لطفا عکس را انتخاب کنید";
            Code = ServiceCode.Error.ToString();
            return Page();
        }

        if (ModelState.IsValid)
        {
            var result = await _productService.Add(Product);
            if (result.Code == 0)
            {
                foreach (var upload in Uploads)
                {
                    var resultImage = await _imageService.Add(upload, result.ReturnData.Id, "Images/Products",
                        _environment.ContentRootPath);
                    if (resultImage.Code > 0)
                    {
                        Message = resultImage.Message;
                        Code = resultImage.Code.ToString();
                        ModelState.AddModelError("", resultImage.Message);
                        await Initial();
                        return Page();
                    }
                }

                return RedirectToPage("/Products/Index",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            }

            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        await Initial();
        return Page();
    }


    public JsonResult OnGetChildrenCategory(int code, List<TreeViewModel> categoriesTreeViewModels)
    {
        var childrenCategory = categoriesTreeViewModels.Where(p => p.ParentId == code);

        return new JsonResult(childrenCategory);
    }
}