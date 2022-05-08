using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

using Entities;
using Entities.Helper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;

using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.Discounts
{
    public class CreateModel : PageModel
    {
        private readonly IDiscountService _discountService;

        public CreateModel(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [BindProperty] public Discount Discount { get; set; }

        [TempData] public string Message { get; set; }

        [TempData] public string Code { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await _discountService.Add(Discount);
                if (result.Code == 0)
                    return RedirectToPage("/Discounts/Index",
                        new { area = "Admin", message = result.Message, code = result.Code.ToString() });
                Message = result.Message;
                Code = result.Code.ToString();
                ModelState.AddModelError("", result.Message);
            }

            return Page();
        }
    }
}