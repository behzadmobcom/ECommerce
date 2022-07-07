using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ArshaHamrah.Areas.Admin.Pages.SlideShows;

public class EditModel : PageModel
{
    private readonly IHostEnvironment _environment;
    private readonly IImageService _imageService;
    private readonly ISlideShowService _slideShowService;

    public EditModel(ISlideShowService slideShowService, IHostEnvironment environment, IImageService imageService)
    {
        _slideShowService = slideShowService;
        _environment = environment;
        _imageService = imageService;
    }

    [BindProperty] public IFormFile Upload { get; set; }
    [BindProperty] public SlideShowViewModel SlideShow { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public async Task OnGet(int id)
    {
        var result = await _slideShowService.GetById(id);
        SlideShow = result.ReturnData;
    }

    public async Task<IActionResult> OnPost()
    {
        var resultOld = await _slideShowService.GetById(SlideShow.Id);
        var slideShowOld = resultOld.ReturnData;
        if (Upload != null)
        {
            var fileName = (await _imageService.Upload(Upload, "Images/SlideShows", _environment.ContentRootPath))
                .ReturnData;
            SlideShow.ImagePath = $"/{fileName[0]}/{fileName[1]}/{fileName[2]}";
        }

        if (Upload == null && SlideShow.ImagePath == null && slideShowOld.ImagePath != SlideShow.ImagePath)
        {
            Message = "لطفا عکس را انتخاب کنید";
            Code = ServiceCode.Error.ToString();
            return Page();
        }

        ModelState["Upload"].ValidationState = ModelValidationState.Valid;
        if (ModelState.IsValid)
        {
            var result = await _slideShowService.Edit(SlideShow);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                return RedirectToPage("/SlideShows/Index",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        return Page();
    }
}