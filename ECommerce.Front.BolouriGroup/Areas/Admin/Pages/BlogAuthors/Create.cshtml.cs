using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.BlogAuthors;

public class CreateModel : PageModel
{
    private readonly IBlogAuthorService _blogAuthorService;
    private readonly IHostEnvironment _environment;
    private readonly IImageService _imageService;

    public CreateModel(IBlogAuthorService blogAuthorService, IImageService imageService, IHostEnvironment environment)
    {
        _blogAuthorService = blogAuthorService;
        _imageService = imageService;
        _environment = environment;
    }

    [BindProperty] public BlogAuthor BlogAuthor { get; set; }

    [BindProperty] public IFormFile Upload { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (Upload == null)
        {
            Message = "لطفا عکس را انتخاب کنید";
            Code = ServiceCode.Error.ToString();
            return Page();
        }

        var fileName = (await _imageService.Upload(Upload, "Images/BlogAuthors", _environment.ContentRootPath))
            .ReturnData;
        BlogAuthor.ImagePath = $"/{fileName[0]}/{fileName[1]}/{fileName[2]}";

        if (ModelState.IsValid)
        {
            var result = await _blogAuthorService.Add(BlogAuthor);
            if (result.Code == 0)
                return RedirectToPage("/BlogAuthors/Index",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        return Page();
    }
}