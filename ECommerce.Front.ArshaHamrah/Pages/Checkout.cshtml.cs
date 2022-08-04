using ECommerce.Front.ArshaHamrah.Utilities;
using ECommerce.Services.IServices;
using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Specialized;
using ServiceReferenceMellat;
using System.Text;
using ZarinpalSandbox;

namespace ArshaHamrah.Pages;

public class CheckoutModel : PageModel
{
    private readonly ICartService _cartService;
    private readonly IPurchaseOrderService _purchaseOrderService;
    private readonly ICityService _cityService;
    private readonly ISendInformationService _sendInformationService;
    private readonly IStateService _stateService;

    public CheckoutModel(ICityService cityService, IStateService stateService,
        ISendInformationService sendInformationService, ICartService cartService,
        IPurchaseOrderService purchaseOrderService)
    {
        _cityService = cityService;
        _stateService = stateService;
        _sendInformationService = sendInformationService;
        _cartService = cartService;
        _purchaseOrderService = purchaseOrderService;
    }

    public ServiceResult<List<State>> StateList { get; set; }
    public ServiceResult<List<City>> CityList { get; set; }
    public ServiceResult<List<PurchaseOrderViewModel>> CartList { get; set; }

    [TempData] public string Message { get; set; }

    [BindProperty] public SendInformation SendInformation { get; set; }

    public SelectList SendInformationList { get; set; }

    public int PostPrice { get; set; }
    [TempData] public string Code { get; set; }

    public int SumPrice { get; set; }

    public async Task OnGet()
    {
        await Initial();
    }

    private async Task Initial()
    {
        StateList = await _stateService.Load();
        CityList = await _cityService.Load(StateList.ReturnData.FirstOrDefault().Id);
        CartList = await _cartService.Load(HttpContext);

        var cartCount = CartList.ReturnData.Sum(x => x.Quantity) - 2;
        PostPrice = 18000 + (cartCount * 2000);

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

    public async Task<IActionResult> OnPost(string Portal)
    {
        var returnAction = "PaymentSuccessfulMellat";
        var url = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/";
        SendInformation.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
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

        //CartList = await _cartService.Load(HttpContext);
        //var amount = CartList.ReturnData.Sum(x => x.SumPrice);
        var resultCart = await _cartService.CartListFromServer();
        if (resultCart.Code > 0)
        {
            Message = resultCart.Message;
            Code = resultCart.Code.ToString();
        }
        var cart = resultCart.ReturnData;
        decimal tempSumPrice = cart.Sum(x => x.SumPrice);
        SumPrice = Convert.ToInt32(tempSumPrice)*10;
        SumPrice = 100000;
        var purchaseOrder = (await _purchaseOrderService.GetByUserId()).ReturnData;
        purchaseOrder.Amount = tempSumPrice;
        purchaseOrder.SendInformationId = SendInformation.Id;
        if (resultSendInformation == 0)
        {
            string description = "";
            switch (Portal)
            {
                case "zarinpal":
                    //Zarinpal

                    description = "خرید تستی ";

                    var paymentZarinpal = await new Payment(SumPrice).PaymentRequest(description, url + returnAction + "?Factor=" + purchaseOrder.Id);
                    if (paymentZarinpal.Status == 100)
                    {

                        await _purchaseOrderService.Edit(purchaseOrder);
                        return Redirect(paymentZarinpal.Link);
                    }
                    else
                    {  //return errorPage;
                        return RedirectToPage("Error");
                    }
                case "mellat":
                    //Zarinpal
                    var date = DateTime.Now.ToString("yyyyMMdd");
                    var time = DateTime.Now.ToString("HHmmss");
                    long terminalId = 6547305;
                    var userName = "Arshahamrah110";
                    var password = "79916117";
                    var paymentMellat = new PaymentGatewayClient();
                    description = "خرید تستی ";
                    var result = await paymentMellat.bpPayRequestAsync(
                        terminalId,
                        userName,
                        password,
                        purchaseOrder.Id,
                        SumPrice,
                        date,
                        time,
                        "",
                        url + returnAction,
                        "0", "", "", "", "", "");
                    var returnCode = result.Body.@return.Split(",");
                    if (returnCode[0] == "0")
                    {
                        await _purchaseOrderService.Edit(purchaseOrder);
                        //var address = "https://bpm.shaparak.ir/pgwchannel/startpay.mellat";
                        //NameValueCollection collection = new NameValueCollection();
                        //collection.Add("RefId", returnCode[1]);
                        //var form = MellatHelper.PreparePOSTForm(address, collection);
                        //await Response.WriteAsync(form);
                        //return Redirect($"{address}?RefId={returnCode[1]}");
                        string refId = returnCode[1];
                        return RedirectToPage("RedirectToMellat", new { refId = refId });
                    }
                    else
                    //return errorPage;
                    {
                        var message = MellatErrorCode.GetMessage(result.Body.@return);
                        return RedirectToPage("Error", new { message = message });
                    }
                    break;
            }

            Message = "خطا هنگام اتصال به درگاه بانکی";
        }
        else
        {
            if (string.IsNullOrEmpty(Message) || Message.Equals("\r\n"))
                Message = "لطفا اطلاعات آدرس را تکمیل کنید یا از لیست یک آدرس را انتخاب کنید";
        }
        Code = resultSendInformation.ToString();
        await Initial();
        return Page();
    }

}