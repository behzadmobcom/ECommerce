using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Front.BolouriGroup.Models;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using ZarinpalSandbox;

namespace ECommerce.Front.BolouriGroup.Pages;

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

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public int SumPrice { get; set; }

    public CheckoutModel(ICartService cartService, ICityService cityService, ISendInformationService sendInformationService, IStateService stateService, IPurchaseOrderService purchaseOrderService)
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
        if (resultCart.Code > 0)
        {
            Message = resultCart.Message;
            Code = resultCart.Code.ToString();
        }
        var cart = resultCart.ReturnData;
        decimal tempSumPrice = cart.Sum(x => x.SumPrice);
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

    public async Task<IActionResult> OnPost(string Portal, int PostPrice)
    {
        string returnAction = "Invoice";
        string url = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/";
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
        
                case "sadad":
                    SumPrice *= 10;
                    purchaseOrder.OrderGuid = Guid.NewGuid();
                    byte[] gb = purchaseOrder.OrderGuid.ToByteArray();
                    purchaseOrder.OrderId = BitConverter.ToInt64(gb, 0);
                    var date = DateTime.Now.ToString("yyyyMMdd");
                    var time = DateTime.Now.ToString("HHmmss");
                    long terminalId = 6547305;
                    var merchantId = "24102279";
                    var merchantKey = "CSlQf8zTne2YH3mnrbwAnKx3rl9ckHKz";

                    var dataBytes = Encoding.UTF8.GetBytes(string.Format("{0};{1};{2}", terminalId, purchaseOrder.OrderGuid, SumPrice));
                    var symmetric = SymmetricAlgorithm.Create("TripleDes");
                    symmetric.Mode = CipherMode.ECB;
                    symmetric.Padding = PaddingMode.PKCS7;
                    var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(merchantKey), new byte[8]);
                    var signData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));
                    
                    var ipgUri = "https://sadad.shaparak.ir/api/v0/Request/PaymentRequest";
                    var data = new
                    {
                        terminalId,
                        merchantId,
                        SumPrice,
                        purchaseOrder.OrderGuid,
                        LocalDateTime = DateTime.Now,   
                        returnUrl = url + returnAction,
                        signData
                    };

                    var res = CallApi<PayResultData>(ipgUri, data);
                    res.Wait();

                    if (res.Result.ResCode == "0")
                    {
                        await _purchaseOrderService.Edit(purchaseOrder);
                        return RedirectToPage("RedirectToSadad", new { redirectUrl = returnAction ,token = res.Result.Token });
                    }
                    return RedirectToPage("Error", new { message = res.Result.Description });
            }

            Message = "خطا هنگام اتصال به درگاه بانکی";
        }
        else
        {
            if (string.IsNullOrEmpty(Message) || Message.Equals("\n\r"))
                Message = "لطفا اطلاعات آدرس را تکمیل کنید یا از لیست یک آدرس را انتخاب کنید";
        }
        Code = resultSendInformation.ToString();
        await Initial();
        return Page();
    }

    public static async Task<T> CallApi<T>(string apiUrl, object value)
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
        using (var client = new HttpClient())
        {

            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            var w = client.PostAsJsonAsync(apiUrl, value);
            w.Wait();
            HttpResponseMessage response = w.Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadFromJsonAsync<T>();
                result.Wait();
                return result.Result;
            }
            return default(T);
        }
    }
}