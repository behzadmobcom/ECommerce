using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.SlideShows;

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

    public async Task<IActionResult> OnGet(string message = null, string code = null)
    {
        Message = message;
        Code = code;
        var result = await _slideShowService.Load();
        if (result.Code == ServiceCode.Success)
        {
            if (Message != null)
            {
                Message = Message;
                Code = Code;
            }
            else
            {
                Message = result.Message;
                Code = result.Code.ToString();
            }
            SlideShows = result.ReturnData;
            return Page();
        }
        return RedirectToPage("/index", new { message = result.Message, code = result.Code.ToString() });      
    }
}