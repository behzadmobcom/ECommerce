using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.Currencies;

public class IndexModel : PageModel
{
    private readonly ICurrencyService _currencyService;

    public IndexModel(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    public List<Currency> Currencies { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task OnGet(string message = null, string code = null)
    {
        Message = message;
        Code = code;
        var result = await _currencyService.Load();
        Currencies = result.ReturnData;
    }
}