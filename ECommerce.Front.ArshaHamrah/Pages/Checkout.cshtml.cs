using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ECommerce.Services.IServices;
using ZarinpalSandbox;

namespace ArshaHamrah.Pages;

public class CheckoutModel : PageModel
{
    private readonly ICartService _cartService;
    private readonly ICityService _cityService;
    private readonly ISendInformationService _sendInformationService;
    private readonly IStateService _stateService;

    public CheckoutModel(ICityService cityService, IStateService stateService,
        ISendInformationService sendInformationService, ICartService cartService)
    {
        _cityService = cityService;
        _stateService = stateService;
        _sendInformationService = sendInformationService;
        _cartService = cartService;
    }

    public ServiceResult<List<State>> StateList { get; set; }
    public ServiceResult<List<City>> CityList { get; set; }
    public ServiceResult<List<PurchaseOrderViewModel>> CartList { get; set; }

    [TempData] public string Message { get; set; }

    [BindProperty] public SendInformation SendInformation { get; set; }

    public SelectList SendInformationList { get; set; }

    public int PostPrice { get; set; }
    [BindProperty] public int Portal { get; set; } = 1;

    public async Task OnGet()
    {
        StateList = await _stateService.Load();
        CityList = await _cityService.Load(StateList.ReturnData.FirstOrDefault().Id);
        CartList = await _cartService.Load(HttpContext);
       
        var cartCount = CartList.ReturnData.Sum(x => x.Quantity)-2;
        PostPrice = 18000 + (cartCount*2000);

        var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
        var sendInformationList = (await _sendInformationService.Load()).ReturnData;
        SendInformationList = new SelectList(sendInformationList, nameof(SendInformation.Id),
            nameof(SendInformation.RecipientName));
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
        var returnAction = "PaymentSuccessful";
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
        if (resultSendInformation == 0)
        {
            switch (Portal)
            {
                case 1:
                    //Zarinpal
                    string result;
                    var payment = await new Payment(amount).PaymentRequest("عنوان", url + returnAction);
                    if (payment.Status == 100)
                        return Redirect(payment.Link);
                    else
                        //return errorPage;
                        return RedirectToPage("Error");
                    //var zarinpal = new PaymentGatewayImplementationServicePortTypeClient();
                    //var code = zarinpal.PaymentRequest("eae355e2-6c18-11e6-a1fa-000c295eb8fc",
                    //    Convert.ToInt32(factorViewModel.SumPrice), factorViewModel.Description,
                    //    factorViewModel.User.Email, factorViewModel.User.Mobile,
                    //    "http://" + Request.Url.Authority + "/Home/VerifyZarinpal?Factor=" +
                    //    factorId.ToString().Encrypt().UrlEncode(), out result);
                    //if (code == 100)
                    //{
                    //    //return Redirect("https://www.zarinpal.com/pg/StartPay/" + result);
                    //    return Redirect("https://www.zarinpal.com/pg/StartPay/" + result + "/ZarinGate");
                    //}
                    break;
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