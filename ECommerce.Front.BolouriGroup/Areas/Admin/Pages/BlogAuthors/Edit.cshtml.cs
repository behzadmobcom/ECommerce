using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.BlogAuthors;

public class EditModel : PageModel
{
    private readonly IBlogAuthorService _blogAuthorService;
    private readonly IHostEnvironment _environment;
    private readonly IImageService _imageService;

    public EditModel(IBlogAuthorService blogAuthorService, IHostEnvironment environment, IImageService imageService)
    {
        _blogAuthorService = blogAuthorService;
        _environment = environment;
        _imageService = imageService;
    }

    [BindProperty] public IFormFile? Upload { get; set; }
    [BindProperty] public BlogAuthor BlogAuthor { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public async Task OnGet(int id)
    {
        var result = await _blogAuthorService.GetById(id);
        BlogAuthor = result.ReturnData;
    }

    public async Task<IActionResult> OnPost()
    {
        if (Upload != null)
        {
            var fileName = (await _imageService.Upload(Upload, "Images/BlogAuthors", _environment.ContentRootPath))
                .ReturnData;
            if (fileName == null)
            {
                ModelState.AddModelError("IvalidFileExtention", "فرمت فایل پشتیبانی نمی‌شود.");
                return Page();
            }
            BlogAuthor.ImagePath = $"/{fileName[0]}/{fileName[1]}/{fileName[2]}";
        }

        if (Upload == null && BlogAuthor.ImagePath == null)
        {
            Message = "لطفا عکس را انتخاب کنید";
            Code = ServiceCode.Error.ToString();
            return Page();
        }

        if (ModelState.IsValid)
        {
            var result = await _blogAuthorService.Edit(BlogAuthor);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                return RedirectToPage("/BlogAuthors/Index",
                    new { area = "Admin", message = result.Message, code = result.Code.ToString() });
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        return Page();
    }
}