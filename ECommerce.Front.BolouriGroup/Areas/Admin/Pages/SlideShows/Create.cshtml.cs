using ECommerce.Services.IServices;
using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.SlideShows;

public class CreateModel : PageModel
{
    private readonly IHostEnvironment _environment;
    private readonly IImageService _imageService;
    private readonly IProductService _productService;
    private readonly ISlideShowService _slideShowService;
    private readonly ICategoryService _categoryService;

    public CreateModel(ISlideShowService slideShowService, IImageService imageService, IHostEnvironment environment,
        IProductService productService, ICategoryService categoryService)
    {
        _slideShowService = slideShowService;
        _imageService = imageService;
        _environment = environment;
        _productService = productService;
        _categoryService = categoryService;
    }

    [BindProperty] public SlideShow SlideShow { get; set; }

    [BindProperty] public IFormFile Upload { get; set; }

    //public PaginationViewModel PaginationViewModel { get; set; }
    public ServiceResult<List<ProductIndexPageViewModel>> Products { get; set; }
    public List<CategoryParentViewModel> Categories { get; set; }
    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task OnGet(string search)
    {
        var result = await _productService.Search(search, 1, 30);
        if (result.Code == ServiceCode.Success)
        {
            Message = result.Message;
            Code = result.Code.ToString();
            Products = result;
        }
        var resultCategory = await _categoryService.GetParents();
        Categories = resultCategory.ReturnData;
    }

    public async Task<IActionResult> OnPost(int selectItem)
    {
        if (selectItem == 1)
        {
            SlideShow.CategoryId = null;
        }
        else
        {
            SlideShow.ProductId = null;
        }
        if (Upload == null)
        {
            Message = "لطفا عکس را انتخاب کنید";
            Code = ServiceCode.Error.ToString();
            return Page();
        }

        var fileName = (await _imageService.Upload(Upload, "Images/SlideShows", _environment.ContentRootPath))
            .ReturnData;
        SlideShow.ImagePath = $"/{fileName[0]}/{fileName[1]}/{fileName[2]}";
        ModelState.Remove("SlideShow.ImagePath");

        if (ModelState.IsValid)
        {
            var result = await _slideShowService.Add(SlideShow);
            if (result.Code == 0)
                return RedirectToPage("/SlideShows/Index",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        Products = await _productService.Search("", 1, 30);
        var resultCategory = await _categoryService.GetParents();
        Categories = resultCategory.ReturnData;

        return Page();
    }

    public async Task<JsonResult> OnGetReturnProducts(string search = "")
    {
        var result = await _productService.Search(search, 1, 30);
        if (result.Code == ServiceCode.Success)
        {
            Message = result.Message;
            Code = result.Code.ToString();
        }
        return new JsonResult(result.ReturnData);

    }
}
