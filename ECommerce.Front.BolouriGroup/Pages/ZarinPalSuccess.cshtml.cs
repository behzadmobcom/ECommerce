using ECommerce.Front.BolouriGroup.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Pages
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class ZarinPalSuccessModel : PageModel
    {
        public IActionResult OnGet(string factor, string status, string authority)
        {
            return RedirectToPage("Invoice", "PayZarinpal", 
                new
            {
                factor = factor,
                status = status,
                authority = authority
            });
        }
    }
}
