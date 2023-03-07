
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages;

public class PurchaseModel : PageModel
{
    private readonly IPurchaseOrderService _purchaseOrderService;
    private readonly IUserService _userService;
    public PurchaseModel(IPurchaseOrderService purchaseOrderService, IUserService userService)
    {
        _purchaseOrderService = purchaseOrderService;
        _userService = userService;
    }
    [BindProperty] public ServiceResult<List<PurchaseListViewModel>> PurchaseOrders { get; set; }
    [BindProperty] public decimal? MaximumAmount { get; set; } = null;
    [BindProperty] public decimal? MinimumAmount { get; set; } = null;
    [BindProperty] public int? IsPaid { get; set; } = null;
    [BindProperty] public int? UserId { get; set; } = null;
    [BindProperty] public Status? Status { get; set; } = null;

    [BindProperty] public ServiceResult<List<UserListViewModel>>? Users{ get; set; }

    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }


    public async Task<IActionResult> OnGet(int id, string search = "", int pageNumber = 1, int pageSize = 10,
    string message = null, string code = null, bool? isPaid=null, decimal? minimumAmount=null, decimal? maximumAmount=null, Status status=Ecommerce.Entities.Status.New)
    {
        Message=message;
        Code=code;
        Users = await _userService.UserList();      
        var result = await _purchaseOrderService.PurchaseList(userId:id, search, pageNumber, pageSize, 
                                                isPaied: isPaid, maximumAmount:maximumAmount, minimumAmount:minimumAmount, statusId:(int)status);
        if (result.Code == ServiceCode.Success)
        {
            Message = result.Message;
            Code = result.Code.ToString();
            PurchaseOrders = result;
            return Page();
        }

        return RedirectToPage("/index", new { message = result.Message, code = result.Code.ToString() });
    }
    public async Task<IActionResult> OnPost(int? UserId, int? IsPaid, decimal? MinimumAmount, decimal? MaximumAmount, Status Status)
    {
        bool? _isPaid = null;
         switch (IsPaid)
        {
            case 0: _isPaid = null; break;
            case 1: _isPaid = true; break;
            case 2: _isPaid = false; break;
        }         
       
        return RedirectToPage("/Users/Purchases",
                   new { area = "Admin", id = UserId!,
                       IsPaid = _isPaid, MinimumAmount=MinimumAmount, MaximumAmount=MaximumAmount, Status=Status,
                       PageSize = 50
                   });
    }
}