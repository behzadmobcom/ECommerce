using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ArshaHamrah.Areas.Admin.Pages.SlideShows;

[Authorize(Roles = "Admin,SuperAdmin")]
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