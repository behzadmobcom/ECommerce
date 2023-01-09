using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Pages
{
    public class RedirectToSadadModel : PageModel
    {
        public string Token { get; set; }
        public string RedirectUrl { get; set; }

        public void OnGet(string redirectUrl, string token)
        {
            RedirectUrl = redirectUrl;
            Token = token;
        }
    }
}
