using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.BlogAuthors
{
    public class IndexModel : PageModel
    {

        public async Task OnGet(string message = null, string code = null)
        {
        }
    }
}