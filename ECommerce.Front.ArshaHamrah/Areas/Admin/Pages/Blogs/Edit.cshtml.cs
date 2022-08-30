using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ArshaHamrah.Areas.Admin.Pages.Blogs;

[Authorize(Roles = "Admin,SuperAdmin")]
public class EditModel : PageModel
{
    private readonly IBlogService _blogService;
    private readonly IHostEnvironment _environment;
    private readonly IImageService _imageService;

    public EditModel(IBlogService blogService, IHostEnvironment environment, IImageService imageService)
    {
        _blogService = blogService;
        _environment = environment;
        _imageService = imageService;
    }

    [BindProperty] public Blog Blog { get; set; }
    [BindProperty] public IFormFile Upload { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public async Task OnGet(int id)
    {
        var result = await _blogService.GetById(id);
        Blog = result.ReturnData;
    }

    public async Task<IActionResult> OnPost()
    {
        if (Upload != null)
        {
            var fileName = (await _imageService.Upload(Upload, "Images/Blogs", _environment.ContentRootPath))
                .ReturnData;
            //Blog.Image = $"/{fileName[0]}/{fileName[1]}/{fileName[2]}";
        }

        if (Upload == null && Blog.Image == null)
        {
            Message = "لطفا عکس را انتخاب کنید";
            Code = ServiceCode.Error.ToString();
            return Page();
        }

        if (ModelState.IsValid)
        {
            var result = await _blogService.Edit(Blog);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                return RedirectToPage("/Blogs/Index",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        return Page();
    }
}