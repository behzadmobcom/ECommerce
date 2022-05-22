using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;

namespace Bolouri.Areas.Admin.Pages.Currencies;

public class CreateModel : PageModel
{
    private readonly ICurrencyService _currencyService;

    public CreateModel(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    [BindProperty] public Currency Currency { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var result = await _currencyService.Add(Currency);
            if (result.Code == 0)
                return RedirectToPage("/Currencies/Index",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        return Page();
    }
}