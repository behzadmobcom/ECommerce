using Ecommerce.Entities.Helper;
using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ecommerce.Entities.ViewModel;

namespace ECommerce.Front.BolouriGroup.Pages;

public class ProfileModel : PageModel
{
    private readonly IUserService _userService;
    private readonly IPurchaseOrderService _purchaseOrderService;
    private readonly IStateService _stateService;
    private readonly ICityService _cityService;


    [BindProperty] public User UserInformation { get; set; }
    public List<PurchaseListViewModel> PurchaseOrders { get; set; }
    public ServiceResult<List<State>> StateList { get; set; }
    public ServiceResult<List<City>> CityList { get; set; }
    public string Message { get; set; }
    public string Code { get; set; }

    public ProfileModel(ICityService cityService, IStateService stateService, IUserService userService, IPurchaseOrderService purchaseOrderService)
    {
        _stateService = stateService;
        _userService = userService;
        _purchaseOrderService = purchaseOrderService;
        _cityService = cityService;
    }
    public async Task OnGet()
    {
        await Initial();
    }
    public async Task<IActionResult> OnPostEdit()
    {
        var resultUser = await _userService.GetUser();
        var editUser = new User();
        if (resultUser.Code == ServiceCode.Success)
        {
            editUser = resultUser.ReturnData;
        }

        editUser.CityId = UserInformation.CityId;
        editUser.StateId = UserInformation.StateId;
        editUser.FirstName = UserInformation.FirstName;
        editUser.LastName = UserInformation.LastName;
        editUser.NationalCode = UserInformation.NationalCode;
        editUser.Email = UserInformation.Email;
        editUser.CompanyName = UserInformation.CompanyName;
        editUser.Mobile = UserInformation.Mobile;
        var result = await _userService.Update(editUser);
        Message = result.Message;
        Code = result.Code.ToString();
        await Initial();
        return Page();
    }

    public async Task<IActionResult> OnGetChangePassword(string oldPass, string newPass, string newConPass)
    {
        var result = await _userService.ChangePassword(oldPass, newPass, newConPass);
        return new JsonResult(result);
    }

    private async Task Initial()
    {
        var resultUser = await _userService.GetUser();
        if (resultUser.Code == ServiceCode.Success)
        {
            UserInformation = resultUser.ReturnData;
        }
        var resultPurchaseOrder = await _purchaseOrderService.PurchaseList(UserInformation.Id);
        if (resultPurchaseOrder.Code == ServiceCode.Success)
        {
            PurchaseOrders = resultPurchaseOrder.ReturnData;
        }
        StateList = await _stateService.Load();
        var stateId = UserInformation.StateId ?? StateList.ReturnData.FirstOrDefault().Id;
        CityList = await _cityService.Load(stateId);
    }

}