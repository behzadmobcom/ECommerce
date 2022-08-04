using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.ArshaHamrah.Pages
{
    public class RedirectToMellatModel : PageModel
    {
        public string RefId { get; set; }
        public void OnGet(string refId)
        {
            RefId = refId;
        }
    }
}
