using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.ArshaHamrah.Areas.Admin.Pages.Currencies;

[Authorize(Roles = "Admin,SuperAdmin")]
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