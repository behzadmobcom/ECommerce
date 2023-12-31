using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.PaymentMethods;

public class DetailModel : PageModel
{
    private readonly IPaymentMethodService _paymentMethodService;

    public DetailModel(IPaymentMethodService paymentMethodService)
    {
        _paymentMethodService = paymentMethodService;
    }

    public PaymentMethod PaymentMethod { get; set; }

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
}