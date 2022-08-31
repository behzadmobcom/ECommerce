using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ArshaHamrah.Areas.Admin.Pages.PaymentMethods;

[Authorize(Roles = "Admin,SuperAdmin")]
public class DeleteModel : PageModel
{
    private readonly IPaymentMethodService _paymentMethodService;

    public DeleteModel(IPaymentMethodService paymentMethodService)
    {
        _paymentMethodService = paymentMethodService;
    }

    public PaymentMethod PaymentMethod { get; set; }
    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _paymentMethodService.GetById(id);
        if (result.Code == 0)
        {
            PaymentMethod = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/PaymentMethods/Index",
            new {area = "Admin", message = result.Message, code = result.Code.ToString()});
    }

    public async Task<IActionResult> OnPost(int id)
    {
        if (ModelState.IsValid)
        {
            var result = await _paymentMethodService.Delete(id);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                return RedirectToPage("/PaymentMethods/Index",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
        }

        return Page();
    }
}