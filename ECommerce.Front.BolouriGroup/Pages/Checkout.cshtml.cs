using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;
using ZarinpalSandbox;

namespace Bolouri.Pages;

public class CheckoutModel : PageModel
{
    private readonly ICartService _cartService;
    private readonly ICityService _cityService;
    private readonly ISendInformationService _sendInformationService;
    private readonly IStateService _stateService;

    public ServiceResult<List<State>> StateList { get; set; }
    public ServiceResult<List<City>> CityList { get; set; }
    public ServiceResult<List<PurchaseOrderViewModel>> CartList { get; set; }

    [BindProperty] public SendInformation SendInformation { get; set; }

    public List<SendInformation> SendInformationList { get; set; }

    [BindProperty] public int Portal { get; set; } = 1;

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public int SumPrice { get; set; }
    public CheckoutModel(ICartService cartService, ICityService cityService, ISendInformationService sendInformationService, IStateService stateService)
    {
        _cartService = cartService;
        _cityService = cityService;
        _sendInformationService = sendInformationService;
        _stateService = stateService;
    }
    public async Task OnGet()
    {
        StateList = await _stateService.Load();
        CityList = await _cityService.Load(StateList.ReturnData.FirstOrDefault().Id);
        CartList = await _cartService.Load(HttpContext);
        var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
        SendInformationList = (await _sendInformationService.Load(userId)).ReturnData;
        SumPrice = CartList.ReturnData.Sum(x => x.SumPrice);
    }


    public async Task<JsonResult> OnGetCityLoad(int id)
    {
        var result = await _cityService.Load(id);
        var ret = "";
        foreach (var city in result.ReturnData) ret += $"<option value='{city.Id}'>{city.Name}</option>";
        return new JsonResult(ret);
    }

    public async Task<JsonResult> OnGetSendInformationLoad(int id)
    {
        var result = await _cityService.Load(id);
        var ret = "";
        foreach (var city in result.ReturnData) ret += $"<option value='{city.Id}'>{city.Name}</option>";
        return new JsonResult(ret);
    }

    public async Task<IActionResult> OnPost()
    {
        var returnAction = "Invoice";
        var url = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/";
        SendInformation.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
        var resultSendInformation = ServiceCode.Success;
        if (SendInformation.Id == 0)
        {
            var result = await _sendInformationService.Add(SendInformation);
            resultSendInformation = result.Code;
        }
        else
        {
            var r = await _sendInformationService.Find(SendInformation.Id);
            SendInformation = r.ReturnData;
        }

        CartList = await _cartService.Load(HttpContext);
        var amount = CartList.ReturnData.Sum(x => x.SumPrice);
        var orderId = CartList.ReturnData.FirstOrDefault().Id;
        if (resultSendInformation == 0)
        {
            switch (Portal)
            {
                case 1:
                    //Zarinpal

                    string description = "خرید تستی ";

                    var payment = await new Payment(amount).PaymentRequest(description, url + returnAction+ "?Factor=" + orderId);
                    if (payment.Status == 100)
                        return Redirect(payment.Link);
                    else
                        //return errorPage;
                        return RedirectToPage("Error");
            }

            Message = "خطا هنگام اتصال به درگاه بانکی";
        }
        else
        {
            Message = "اطلاعات شما ثبت نشد";
        }

        return Page();
    }
}