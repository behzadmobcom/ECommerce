using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.Admin.Pages;

public class PurchaseOrderModel : PageModel
{
    private readonly IPurchaseOrderService _purchaseOrderService;
    public PurchaseOrderModel(IPurchaseOrderService purchaseOrderService)
    {
        _purchaseOrderService = purchaseOrderService;
    }
    [BindProperty] public PurchaseOrder PurchaseOrder { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public async Task<IActionResult> OnGet(int id, string message = null, string code = null)
    {
        Message = message;
        Code = code;
        var result = await _purchaseOrderService.GetPurchaseOrderWithIncludeById(id);
        if (result.Code == ServiceCode.Success)
        {
            Message = result.Message;
            Code = result.Code.ToString();
            PurchaseOrder = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/index", new { message = result.Message, code = result.Code.ToString() });
    }
}