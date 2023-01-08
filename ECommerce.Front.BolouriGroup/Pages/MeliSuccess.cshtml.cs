using ECommerce.Front.BolouriGroup.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Pages
{
    [IgnoreAntiforgeryToken]
    public class MeliSuccessModel : PageModel
    {
        public IActionResult OnPost(PurchaseResult result)
        {
            return RedirectToPage("Invoice", result );
        }
    }
}
