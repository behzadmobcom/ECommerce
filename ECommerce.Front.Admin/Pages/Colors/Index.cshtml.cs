using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.Admin.Pages.Colors;

public class IndexModel : PageModel
{
    private readonly IColorService _colorService;

    public IndexModel(IColorService colorService)
    {
        _colorService = colorService;
    }

    public ServiceResult<List<Color>> Colors { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task<IActionResult> OnGet(string search = "", int pageNumber = 1, int pageSize = 10,
        string message = null, string code = null)
    {
        Message = message;
        Code = code;
        var result = await _colorService.GetAll(search, pageNumber, pageSize);
        if (result.Code == ServiceCode.Success)
        {
            Message = result.Message;
            Code = result.Code.ToString();
            Colors = result;
            return Page();
        }

        return RedirectToPage("/index", new {message = result.Message, code = result.Code.ToString()});
    }
}