using System.Threading.Tasks;
using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.Stores
{
    public class EditModel : PageModel
    {
        private readonly IStoreService _storeService;

        public EditModel(IStoreService storeService)
        {
            _storeService = storeService;
        }
        [BindProperty] public Store Store { get; set; }
        [TempData] public string Message { get; set; }
        [TempData] public string Code { get; set; }

        public async Task OnGet(int id)
        {
            var result = await _storeService.GetById(id);
            Store = result.ReturnData;
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await _storeService.Edit(Store);
                Message = result.Message;
                Code = result.Code.ToString();
                if (result.Code == 0)
                    return RedirectToPage("/Stores/Index",
                        new {area = "Admin", message = result.Message, code = result.Code.ToString()});
                Message = result.Message;
                Code = result.Code.ToString();
                ModelState.AddModelError("", result.Message);
            }

            return Page();
        }
    }
}