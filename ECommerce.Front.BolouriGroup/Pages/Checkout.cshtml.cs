using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;
using ZarinpalSandbox;

namespace Bolouri.Pages;

public class CheckoutModel : PageModel
{
    private readonly ICityService _cityService;
    private readonly ISendInformationService _sendInformationService;
    private readonly IStateService _stateService;
    private readonly IPurchaseOrderService _purchaseOrderService;
    private readonly ICartService _cartService;
    

    [BindProperty] public List<State> StateList { get; set; }
    [BindProperty] public List<City> CityList { get; set; }

    [BindProperty] public SendInformation SendInformation { get; set; }

    [BindProperty] public List<SendInformation> SendInformationList { get; set; }

    [BindProperty] public int Portal { get; set; } = 1;

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public int SumPrice { get; set; }

    public CheckoutModel(ICartService cartService,ICityService cityService, ISendInformationService sendInformationService, IStateService stateService, IPurchaseOrderService purchaseOrderService)
    {
        _cityService = cityService;
        _sendInformationService = sendInformationService;
        _stateService = stateService;
        _purchaseOrderService = purchaseOrderService;
        _cartService = cartService;
    }
    public async Task OnGet()
    {
        await Initial();
    }
    private async Task Initial()
    {
        StateList = (await _stateService.Load()).ReturnData;
        CityList = (await _cityService.Load(StateList.First().Id)).ReturnData;
        SendInformationList = (await _sendInformationService.Load()).ReturnData;
        var resultCart = await _cartService.CartListFromServer();
        if(resultCart.Code>0)
        {
            Message = resultCart.Message;
            Code = resultCart.Code.ToString();
        }
        var cart = resultCart.ReturnData;
        decimal tempSumPrice = cart.Sum(x=>x.SumPrice);
        SumPrice = Convert.ToInt32(tempSumPrice);
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
        //SendInformation.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
        var resultSendInformation = ServiceCode.Success;
        if (SendInformation.Id == 0)
        {
            var result = await _sendInformationService.Add(SendInformation);
            resultSendInformation = result.Code;
            Message = result.Message;
            SendInformation = result.ReturnData;
        }
        else
        {
            var r = await _sendInformationService.Find(SendInformation.Id);
            SendInformation = r.ReturnData;
        }

        var resultCart = await _cartService.CartListFromServer();
        if (resultCart.Code > 0)
        {
            Message = resultCart.Message;
            Code = resultCart.Code.ToString();
        }
        var cart = resultCart.ReturnData;
        decimal tempSumPrice = cart.Sum(x => x.SumPrice);
        SumPrice = Convert.ToInt32(tempSumPrice);

        var purchaseOrder = (await _purchaseOrderService.GetByUserId()).ReturnData;
        purchaseOrder.Amount = tempSumPrice;
        purchaseOrder.SendInformationId = SendInformation.Id;
        if (resultSendInformation == 0)
        {
            switch (Portal)
            {
                case 1:
                    //Zarinpal

                    string description = "خرید تستی ";

                    var payment = await new Payment(SumPrice).PaymentRequest(description, url + returnAction + "?Factor=" + purchaseOrder.Id);
                    if (payment.Status == 100)
                    {

                        await _purchaseOrderService.Edit(purchaseOrder);
                        return Redirect(payment.Link);
                    }
                    else
                        //return errorPage;
                        return RedirectToPage("Error");
            }

            Message = "خطا هنگام اتصال به درگاه بانکی";
        }
        else
        {
            if (string.IsNullOrEmpty(Message) ||  Message.Equals("\n\r"))
                Message = "لطفا اطلاعات آدرس را تکمیل کنید یا از لیست یک آدرس را انتخاب کنید";
        }
        Code = resultSendInformation.ToString();
        await Initial();
        return Page();
    }
}