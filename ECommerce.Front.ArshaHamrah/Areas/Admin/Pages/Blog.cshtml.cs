using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.ArshaHamrah.Areas.Admin.Pages;

[Authorize(Roles = "Admin,SuperAdmin")]
public class BlogModel : PageModel
{
    public void OnGet()
    {
    }
}