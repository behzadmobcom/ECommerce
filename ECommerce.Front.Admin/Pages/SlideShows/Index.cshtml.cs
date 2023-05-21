using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.Admin.Pages.SlideShows;

public class IndexModel : PageModel
{
    private readonly ISlideShowService _slideShowService;

    public IndexModel(ISlideShowService slideShowService)
    {
        _slideShowService = slideShowService;
    }

    public List<SlideShowViewModel> SlideShows { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task OnGet(string message = null, string code = null)
    {
        Message = message;
        Code = code;
        var result = await _slideShowService.Load();
        SlideShows = result.ReturnData;
    }
}