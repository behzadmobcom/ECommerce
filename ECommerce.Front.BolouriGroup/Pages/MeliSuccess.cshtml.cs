using ECommerce.Front.BolouriGroup.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Pages
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class MeliSuccessModel : PageModel
    {
        public IActionResult OnPost(PurchaseResult result)
        {
            return RedirectToPage("invoice", result );
        }
    }
}
