using Ecommerce.Entities;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ECommerce.Front.BolouriGroup.Pages;

public class RegisterModel : PageModel
{
    private readonly IUserService _userService;
    private readonly ICityService _cityService;
    private readonly IStateService _stateService;

    [BindProperty] public RegisterViewModel RegisterViewModel { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }
    [BindProperty] public List<State> StateList { get; set; }
    [BindProperty] public List<City> CityList { get; set; }

    public RegisterModel(IUserService userService, ICityService cityService, IStateService stateService)
    {
        _userService = userService;
        _cityService = cityService;
        _stateService = stateService;
    }

    public async Task OnGet()
    {
        StateList = (await _stateService.Load()).ReturnData;
        CityList = (await _cityService.Load(StateList.First().Id)).ReturnData;
    }

    public async Task<JsonResult> OnGetCityLoad(int id)
    {
        var result = await _cityService.Load(id);
        var ret = "";
        foreach (var city in result.ReturnData) ret += $"<option value='{city.Id}'>{city.Name}</option>";
        return new JsonResult(ret);
    }

    public async Task<IActionResult> OnPostRegister()
    {
        if (!RegisterViewModel.IsRole)
        {
            //          !qa@ws#ed123
            //          !qa@ws#ed123123
            Message = "لطفا ایتدا قوانین و مقررارت را تایید کنید";
            Code = "Error";
            return Page();
        }
        ModelState.Remove("RegisterViewModel.NationalCode");
        if(RegisterViewModel.NationalCode ==  null)
        {
            RegisterViewModel.NationalCode = "0000000000";
        }
        if (!ModelState.IsValid) return Page();
        switch (RegisterViewModel.CompanyType)
        {
           case 10:
               RegisterViewModel.CompanyTypeName = "رستوران";
               break;
           case 11:
               RegisterViewModel.CompanyTypeName = "کافی شاپ";
               break;
           case 9 :
               RegisterViewModel.CompanyTypeName = "هتل";
               break;
            case 16:
                RegisterViewModel.CompanyTypeName = "تالار";
               break;
           case 21:
               RegisterViewModel.CompanyTypeName = "شرکت";
               break;
           case 15:
               RegisterViewModel.CompanyTypeName = "فروشگاه";
               break;
        }

        var result = await _userService.Register(RegisterViewModel);
        if(result.Code>0)
        {
            Message = result.Message;
            Code = result.Code.ToString();
            return Page();
        }
        return RedirectToPage("index");
    }
}