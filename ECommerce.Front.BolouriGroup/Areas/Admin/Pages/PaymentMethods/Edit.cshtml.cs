using System.Threading.Tasks;
using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.PaymentMethods
{
    public class EditModel : PageModel
    {
        private readonly IPaymentMethodService _paymentMethodService;

        public EditModel(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }
        [BindProperty] public PaymentMethod PaymentMethod { get; set; }
        [TempData] public string Message { get; set; }
        [TempData] public string Code { get; set; }

        public async Task OnGet(int id)
        {
            var result = await _paymentMethodService.GetById(id);
            PaymentMethod = result.ReturnData;
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await _paymentMethodService.Edit(PaymentMethod);
                Message = result.Message;
                Code = result.Code.ToString();
                if (result.Code == 0)
                    return RedirectToPage("/PaymentMethods/Index",
                        new {area = "Admin", message = result.Message, code = result.Code.ToString()});
                Message = result.Message;
                Code = result.Code.ToString();
                ModelState.AddModelError("", result.Message);
            }

            return Page();
        }
    }
}