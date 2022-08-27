using ECommerce.Services.IServices;
using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArshaHamrah.Pages;

public class UserProfile : PageModel
{
    private readonly IUserService _userService;
    private readonly IPurchaseOrderService _purchaseOrderService;

    public User User { get; set; }
    public List<PurchaseListViewModel> PurchaseOrders { get; set; }

    public UserProfile(IUserService userService, IPurchaseOrderService purchaseOrderService)
    {
        _userService = userService;
        _purchaseOrderService = purchaseOrderService;
    }

    public async Task OnGet()
    {
        var resultUser = await _userService.GetUser();
        if (resultUser.Code == ServiceCode.Success)
        {
            User = resultUser.ReturnData;
        } 
        var resultPurchaseOrder = await _purchaseOrderService.PurchaseList(User.Id);
        if (resultPurchaseOrder.Code == ServiceCode.Success)
        {
            PurchaseOrders = resultPurchaseOrder.ReturnData;
        }
    }
}