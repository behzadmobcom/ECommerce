using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.Products;

public class EditModel : PageModel
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

    public EditModel(IProductService productService, ITagService tagService, ICategoryService categoryService,
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

    public List<Discount> Discounts { get; set; }
    public List<Store> Stores { get; set; }
    public List<Supplier> Suppliers { get; set; }
    public List<Brand> Brands { get; set; }
    public List<Tag> Tags { get; set; }
    public List<Keyword> Keywords { get; set; }

    public List<CategoryParentViewModel> CategoryParentViewModel { get; set; }

    [BindProperty] public ProductViewModel Product { get; set; }
    [BindProperty] public List<IFormFile> Uploads { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await Initial(id);
        if (result.Code == 0) return Page();
        return RedirectToPage("/Products/Index",
            new {area = "Admin", message = result.Message, code = result.Code.ToString()});
    }

    public async Task<IActionResult> OnPost()
    {
        if (Uploads == null)
        {
            Message = "لطفا عکس را انتخاب کنید";
            Code = ServiceCode.Error.ToString();
            await Initial(Product.Id);
            return Page();
        }
        foreach (var upload in Uploads)
        {
            if (upload.FileName.Split('.').Last().ToLower() != "webp")
            {
                ModelState.AddModelError("IvalidFileExtention", "فرمت فایل پشتیبانی نمی‌شود.");
                await Initial(Product.Id);
                return Page();
            }
        }
        if (ModelState.IsValid)
        {
            var result = await _productService.Edit(Product);
            if (result.Code == 0)
            {
                foreach (var upload in Uploads)
                {
                    var resultImage = await _imageService.Add(upload, Product.Id, "Images/Products",
                        _environment.ContentRootPath);
                    if (resultImage.Code > 0)
                    {
                        Message = resultImage.Message;
                        Code = resultImage.Code.ToString();
                        ModelState.AddModelError("", resultImage.Message);
                        await Initial(Product.Id);
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

        await Initial(Product.Id);
        return Page();
    }

    public async Task<IActionResult> OnPostDeleteImage(string imageName, int id, int productId)
    {
        {
            var result = await _imageService.Delete($"Images/Products/{imageName}", id, _environment.ContentRootPath);

            if (result.Code == 0)
                return RedirectToPage("/Products/Edit",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }
        await Initial(productId);
        return Page();
    }

    private async Task<ServiceResult<ProductViewModel>> Initial(int id)
    {
        var result = await _productService.GetById(id);
        if (result.Code > 0)
            return new ServiceResult<ProductViewModel>
            {
                Code = result.Code,
                Message = result.Message
            };
        Product = result.ReturnData;

        //Product = new ProductViewModel();
        Stores = (await _storeService.Load()).ReturnData;

        Discounts = (await _discountService.Load()).ReturnData;

        Suppliers = (await _supplierService.Load()).ReturnData;

        Brands = (await _brandService.Load()).ReturnData;

        Tags = (await _tagService.GetAll()).ReturnData;

        Keywords = (await _keywordService.GetAll()).ReturnData;

        CategoryParentViewModel = (await _categoryService.GetParents(id)).ReturnData;

        return result;
    }
}