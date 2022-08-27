using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.Tickets;

public class IndexModel : PageModel
{
    public IndexModel()
    {
        
    }
   
    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task OnGet(string search = "", int pageNumber = 1, int pageSize = 10,
        string message = null, string code = null)
    {
        Message = message;
        Code = code;
    }
}