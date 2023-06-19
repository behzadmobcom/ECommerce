
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
    [BindProperty] public string Username { get; set; } = "";
    [BindProperty] public Status? Status { get; set; } = null;
    [BindProperty] public PurchaseSort PurchaseSort { get; set; } = PurchaseSort.HighToLowDateBuying;

    [BindProperty] public ServiceResult<List<UserListViewModel>>? Users{ get; set; }

    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public async Task<IActionResult> OnGet(int userId = 0, string userName="", string search="" , int pageNumber = 1, int pageSize = 10,
    string message = null, string code = null, bool? isPaid=null, decimal? minimumAmount=null, decimal? maximumAmount=null, 
    Status status=Ecommerce.Entities.Status.New, PurchaseSort purchaseSort= PurchaseSort.HighToLowDateBuying)
    {
        Message = message;
        Code = code;
        Username = userName;
      /*  Users = await _userService.UserList(pageSize: 200); */ // It should be corrected in the next task 
        var result = await _purchaseOrderService.PurchaseList(userId: userId, search:userName, pageNumber, pageSize, 
                                                isPaied: isPaid, maximumAmount:maximumAmount, minimumAmount:minimumAmount, statusId:(int)status, purchaseSort:(int)purchaseSort );
        if (result.Code == ServiceCode.Success)
        {
            Message = result.Message;
            Code = result.Code.ToString();
            PurchaseOrders = result;
            return Page();
        }

        return RedirectToPage("/index", new { message = result.Message, code = result.Code.ToString() });
    }
    public async Task<IActionResult> OnPost(string Username, bool? IsPaid, decimal? MinimumAmount, decimal? MaximumAmount, PurchaseSort PurchaseSort, Status Status)
    {
        try
        {
            return RedirectToPage("/Users/Purchases",
                    new
                    {
                        area = "Admin",
                        isPaid = IsPaid,
                        minimumAmount = MinimumAmount,
                        maximumAmount = MaximumAmount,
                        status = Status,
                        purchaseSort = PurchaseSort,
                        userName = Username,
                        pageSize = 10
                    }) ;
        }
        catch (Exception ex)
        {
            return Page();
        }
    }

    public async Task<JsonResult> OnGetEditPurchaseStatus(int id, Status status)
    {
        var result = await _purchaseOrderService.SetStatusById(id, status);
        return new JsonResult(result);
    }

    public async Task<JsonResult> OnGetUserListBySearch(string search)
    {
        var result = await _userService.UserList(search: search);
        List<string> users = result.ReturnData.Select(x=>x.Username).ToList();        
        return new JsonResult(users);
    }

}