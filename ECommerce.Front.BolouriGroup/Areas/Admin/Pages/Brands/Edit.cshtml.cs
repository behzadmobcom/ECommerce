using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;

namespace Bolouri.Areas.Admin.Pages.Brands;

public class EditModel : PageModel
{
    private readonly IBrandService _brandService;
    private readonly IHostEnvironment _environment;
    private readonly IImageService _imageService;

    public EditModel(IBrandService brandService, IHostEnvironment environment, IImageService imageService)
    {
        _brandService = brandService;
        _environment = environment;
        _imageService = imageService;
    }

    [BindProperty] public Brand Brand { get; set; }
    [BindProperty] public IFormFile Upload { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public async Task OnGet(int id)
    {
        var result = await _brandService.GetById(id);
        Brand = result.ReturnData;
    }

    public async Task<IActionResult> OnPost()
    {
        if (Upload != null)
        {
            var fileName = (await _imageService.Upload(Upload, "Images/Brands", _environment.ContentRootPath))
                .ReturnData;
            Brand.ImagePath = $"/{fileName[0]}/{fileName[1]}/{fileName[2]}";
        }

        if (Upload == null && Brand.ImagePath == null)
        {
            Message = "لطفا عکس را انتخاب کنید";
            Code = ServiceCode.Error.ToString();
            return Page();
        }

        if (ModelState.IsValid)
        {
            var result = await _brandService.Edit(Brand);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                return RedirectToPage("/Brands/Index",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        return Page();
    }
}