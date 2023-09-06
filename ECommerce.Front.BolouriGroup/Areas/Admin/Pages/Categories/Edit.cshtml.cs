using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.Categories;

public class EditModel : PageModel
{
    private readonly ICategoryService _categoryService; 
    private readonly IHostEnvironment _environment;
    private readonly IImageService _imageService;

    public EditModel(ICategoryService categoryService, IHostEnvironment environment, IImageService imageService)
    {
        _categoryService = categoryService;
        _environment = environment;
        _imageService = imageService;
    }

    [BindProperty] public Category Category { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }
    [BindProperty] public IFormFile Upload { get; set; }

    public async Task OnGet(int id)
    {
        var result = await _categoryService.GetById(id);
        Category = result.ReturnData;
    }

    public async Task<IActionResult> OnPost()
    {
        if (Upload != null)
        {
            var fileName = (await _imageService.Upload(Upload, "Images/Categories", _environment.ContentRootPath))
                .ReturnData;
            if (fileName == null)
            {
                ModelState.AddModelError("IvalidFileExtention", "فرمت فایل پشتیبانی نمی‌شود.");
                return Page();
            }
            Category.ImagePath = $"/{fileName[0]}/{fileName[1]}/{fileName[2]}";
        }
        if (Upload == null && Category.ImagePath == null)
        {
            Message = "لطفا عکس را انتخاب کنید";
            Code = ServiceCode.Error.ToString();
            return Page();
        }
        ModelState.Remove("Category.ImagePath");

        if (ModelState.IsValid)
        {
            var result = await _categoryService.Edit(Category);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                return RedirectToPage("/Categories/Index",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        return Page();
    }
}