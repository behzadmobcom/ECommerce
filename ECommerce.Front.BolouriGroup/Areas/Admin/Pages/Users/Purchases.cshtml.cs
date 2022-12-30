
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages;

public class PurchaseModel : PageModel
{
    private readonly IPurchaseOrderService _purchaseOrderService;
    public PurchaseModel(IPurchaseOrderService purchaseOrderService)
    {
        _purchaseOrderService = purchaseOrderService;
    }
    [BindProperty] public ServiceResult<List<PurchaseListViewModel>> PurchaseOrder { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public async Task<IActionResult> OnGet(int id, string search = "", int pageNumber = 1, int pageSize = 10,
    string message = null, string code = null)
    {
        Message=message;
        Code=code;
        var result = await _purchaseOrderService.PurchaseList(userId:id, search, pageNumber, pageSize);
        if (result.Code == ServiceCode.Success)
        {
            Message = result.Message;
            Code = result.Code.ToString();
            PurchaseOrder = result;
            return Page();
        }

        return RedirectToPage("/index", new { message = result.Message, code = result.Code.ToString() });
    }
}