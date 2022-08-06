using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.ArshaHamrah.Pages
{
    [IgnoreAntiforgeryToken]
    public class MellatSuccessModel : PageModel
    {
        public IActionResult OnPost(string RefId, string ResCode, long SaleOrderId, long SaleReferenceId, string PanCardHolder, string CreditCardSaleResponseDetail, long FinalAmount)
        {
            return RedirectToPage("PaymentSuccessfulMellat", new { RefId, ResCode, SaleOrderId, SaleReferenceId, PanCardHolder, CreditCardSaleResponseDetail, FinalAmount });
        }
    }
}
