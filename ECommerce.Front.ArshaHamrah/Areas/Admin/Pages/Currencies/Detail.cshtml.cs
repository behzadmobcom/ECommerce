using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.Currencies;

public class DetailModel : PageModel
{
    private readonly ICurrencyService _currencyService;

    public DetailModel(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    public Currency Currency { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _currencyService.GetById(id);
        if (result.Code == 0)
        {
            Currency = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/Currencies/Index",
            new {area = "Admin", message = result.Message, code = result.Code.ToString()});
    }
}