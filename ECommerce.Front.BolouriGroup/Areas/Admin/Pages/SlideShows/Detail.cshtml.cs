using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;

namespace Bolouri.Areas.Admin.Pages.SlideShows;

public class DetailModel : PageModel
{
    private readonly ISlideShowService _slideShowService;

    public DetailModel(ISlideShowService slideShowService)
    {
        _slideShowService = slideShowService;
    }

    public SlideShowViewModel SlideShow { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _slideShowService.GetById(id);
        if (result.Code == 0)
        {
            SlideShow = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/SlideShows/Index",
            new {area = "Admin", message = result.Message, code = result.Code.ToString()});
    }
}