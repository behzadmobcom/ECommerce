using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;
using ZarinpalSandbox;

namespace Bolouri.Pages;

public class InvoiceModel : PageModel
{
    private readonly ICartService _cartService;

    public string Refid { get; set; }
    public InvoiceModel(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<ActionResult> OnGet(string factor, string status, string authority)
    {
        if (string.IsNullOrEmpty(status) == false && string.IsNullOrEmpty(authority) == false &&
            string.IsNullOrEmpty(factor) == false && status.ToLower() == "ok")
        {
            var cartResult = await _cartService.Load(HttpContext);
            var amount = cartResult.ReturnData.Sum(x => x.SumPrice);
            var statusInt = await new Payment(amount).Verification(authority);

            Refid = statusInt.RefId.ToString();
        }

        return RedirectToPage("Error", new { message = "مشکل در درگاه پرداخت" });
    }
}