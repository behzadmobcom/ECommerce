using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ArshaHamrah.Areas.Admin.Pages.PurchaseList;

[Authorize(Roles = "Admin,SuperAdmin")]
public class IndexModel : PageModel
{
    private readonly IPurchaseOrderService _purchaseOrderService;

    public IndexModel(IPurchaseOrderService purchaseOrderService)
    {
        _purchaseOrderService = purchaseOrderService;
    }

    public ServiceResult<List<PurchaseListViewModel>> PurchaseList { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task<IActionResult> OnGet(int userId = 0, string search = "", int pageNumber = 1, int pageSize = 10,
        string message = null, string code = null,int purchaseSort =1,bool? isPaied=null, DateTime? fromCreationDate=null, 
        DateTime? toCreationDate=null, int? statusId=null, decimal? minimumAmount=null, decimal? maximumAmount=null, 
        PaymentMethodStatus? paymentMethodStatus=null)
    {
        Message = message;
        Code = code;
        var result = await _purchaseOrderService.PurchaseList(userId,search, pageNumber,pageSize,purchaseSort,isPaied,
            fromCreationDate,toCreationDate,statusId,minimumAmount,maximumAmount,paymentMethodStatus );
        if (result.Code == ServiceCode.Success)
        {
            Message = result.Message;
            Code = result.Code.ToString();
            PurchaseList = result;
            return Page();
        }

        return RedirectToPage("/index", new {message = result.Message, code = result.Code.ToString()});
    }
}