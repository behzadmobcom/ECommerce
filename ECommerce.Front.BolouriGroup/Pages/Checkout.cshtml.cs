using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Front.BolouriGroup.Models;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;
using PersianDate.Standard;
using ZarinpalSandbox;

namespace ECommerce.Front.BolouriGroup.Pages;

public class CheckoutModel : PageModel
{
    private readonly ICityService _cityService;
    private readonly ISendInformationService _sendInformationService;
    private readonly IStateService _stateService;
    private readonly IPurchaseOrderService _purchaseOrderService;
    private readonly ICartService _cartService;
    private readonly IDiscountService _discountService;


    [BindProperty] public List<State> StateList { get; set; }
    [BindProperty] public List<City> CityList { get; set; }
    [BindProperty] public SendInformation SendInformation { get; set; }

    [BindProperty] public List<SendInformation> SendInformationList { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public int SumPrice { get; set; }
    public ServiceResult<Discount> DiscountResult { get; set; }

    public CheckoutModel(
        ICartService cartService,
        ICityService cityService,
        ISendInformationService sendInformationService,
        IStateService stateService,
        IPurchaseOrderService purchaseOrderService,
        IDiscountService discountService)
    {
        _cityService = cityService;
        _sendInformationService = sendInformationService;
        _stateService = stateService;
        _purchaseOrderService = purchaseOrderService;
        _cartService = cartService;
        _discountService = discountService;
    }

    public async Task OnGet(string message, string code)
    {
        await Initial();
        Message = message;
        Code = code;
    }

    private async Task Initial()
    {
        StateList = (await _stateService.Load()).ReturnData;
        var cityServiceResponse = await _cityService.LoadAllCity();
        CityList = cityServiceResponse.ReturnData;
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

    public async Task<JsonResult> OnGetSendInformationLoad(int id)
    {
        var result = await _cityService.Load(id);
        var ret = "";
        foreach (var city in result.ReturnData) ret += $"<option value='{city.Id}'>{city.Name}</option>";
        return new JsonResult(ret);
    }

    public async Task<JsonResult> OnGetDiscount(string discountCode)
    {
        await Initial();
        var sumPriceResult = await DiscountCalculate(discountCode, SumPrice);
       
        return new JsonResult(sumPriceResult);
    }

    public async Task<JsonResult> OnGetRemoveDiscount()
    {
        await Initial();
        return new JsonResult(SumPrice);
    }

    private async Task<VerifyResultData> DiscountCalculate(string discountCode, int sumPrice)
    {
        DiscountResult = await _discountService.GetByCode(discountCode);
        var discount = DiscountResult.ReturnData;
        VerifyResultData resultData = new();
        if (DiscountResult.Code > 0 || DiscountResult.Status != 200)
        {
            resultData.SumPrice = sumPrice;
            resultData.Succeed = false;
            resultData.Description = "تخفیف مورد نظر یافت نشد";
            return resultData;
        }
        if ( !DiscountResult.ReturnData.IsActive)
        {
            resultData.SumPrice = sumPrice;
            resultData.Succeed = false;
            resultData.Description = "تخفیف مورد نظر دیگر فعال نیست";
            return resultData;

        }
        if (DiscountResult.ReturnData.StartDate?.Date > DateTime.Now.Date)
        {
            resultData.SumPrice = sumPrice;
            resultData.Succeed = false;
            resultData.Description = $"تخفیف مورد نظر هنوز شروع نشده است، لطفا بعد از تاریخ {DiscountResult.ReturnData.StartDate.ToFa()} دوباره تلاش کنید";
            return resultData;
        }
        if (DiscountResult.ReturnData.EndDate?.Date < DateTime.Now.Date)
        {
            resultData.SumPrice = sumPrice;
            resultData.Succeed = false;
            resultData.Description = "تخفیف مورد نظر منقضی شده است";
            return resultData;
        }
        if (discount.Amount is > 0)
        {
            sumPrice -= (int)discount.Amount;
            if (sumPrice < 0) sumPrice = 0;
        }
        else
        {
            if (discount.Percent != null) sumPrice -= (int)((discount.Percent.Value / 100) * sumPrice);
        }
        resultData.SumPrice = sumPrice;
        resultData.Succeed = true;
        resultData.Description = "تخفیف بعد از پرداخت در فاکتور شما لحاظ خواهد شد";
        return resultData;
    }

    public async Task<IActionResult> OnPost(string Portal, int PostPrice, string discountCode)
    {
        await Initial();
        ModelState.Remove("discountCode");
        string returnAction = "melisuccess";
        string url = $"https://{Request.Host}{Request.PathBase}/";
        SendInformation.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
        var resultSendInformation = ServiceCode.Success;
        if (SendInformation.Id == 0)
        {
            ModelState.Remove("SendInformation.Id");
            if (ModelState.IsValid)
            {
                var result = await _sendInformationService.Add(SendInformation);
                resultSendInformation = result.Code;
                Message = result.Message;
                SendInformation = result.ReturnData;
            }
            else
            {
                Message = "لطفا یک آدرس جدید وارد کنید یا یک آدرس را انتخاب کنید";
                Code = "Error";
                return Page();
            }
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

        var discountResult = await DiscountCalculate(discountCode, Convert.ToInt32(tempSumPrice));
        SumPrice= discountResult.SumPrice;
        if (SumPrice >= 50000000)
        {
            Message = "مبلغ سفارش نمی تواند بیشتر از 50 میلیون تومان باشد";
            Code = "Error";
            return Page();
        }
        var purchaseOrder = (await _purchaseOrderService.GetByUserId()).ReturnData;
        purchaseOrder.Amount = tempSumPrice;
        purchaseOrder.SendInformationId = SendInformation.Id;
        if (DiscountResult.Code == 0 && DiscountResult.Status == 200 && DiscountResult.ReturnData.IsActive && (DiscountResult.ReturnData.StartDate?.Date <= DateTime.Now.Date || DiscountResult.ReturnData.StartDate == null) && (DiscountResult.ReturnData.EndDate?.Date >= DateTime.Now.Date || DiscountResult.ReturnData.EndDate == null))
        {
            purchaseOrder.DiscountId = DiscountResult.ReturnData.Id;
            purchaseOrder.DiscountAmount = (int)tempSumPrice - SumPrice;
        }
        if (resultSendInformation == 0)
        {
            string description = "";
            switch (Portal)
            {
                //case "zarinpal":
                //    returnAction = "ZarinPalSuccess";
                //    description = "خرید تستی ";
                //    purchaseOrder.OrderGuid = Guid.NewGuid();
                //    byte[] gb1 = purchaseOrder.OrderGuid.ToByteArray();
                //    purchaseOrder.OrderId = BitConverter.ToInt64(gb1, 0);
                //    var paymentZarinpal = await new Payment(SumPrice).PaymentRequest(description, url + returnAction + "?Factor=" + purchaseOrder.Id);
                //    if (paymentZarinpal.Status == 100)
                //    {
                //        await _purchaseOrderService.Edit(purchaseOrder);
                //        return Redirect(paymentZarinpal.Link);
                //    }
                //    return RedirectToPage("Error");
                case "sadad":
                    SumPrice *= 10;
                    purchaseOrder.OrderGuid = Guid.NewGuid();
                    byte[] gb = purchaseOrder.OrderGuid.ToByteArray();
                    purchaseOrder.OrderId = BitConverter.ToInt64(gb, 0);
                    var date = DateTime.Now.ToString("yyyyMMdd");
                    var time = DateTime.Now.ToString("HHmmss");
                    long merchantId = 000000140341290;//000000140336964;//000000140341290;
                    var terminalId = "24102279";//24095674;// "24102279";
                    var terminalKey = "CSlQf8zTne2YH3mnrbwAnKx3rl9ckHKz";//"8v8AEee8YfZX+wwc1TzfShRgH3O9WOho";// "CSlQf8zTne2YH3mnrbwAnKx3rl9ckHKz";
                    var dataBytes = Encoding.UTF8.GetBytes(string.Format("{0};{1};{2}", terminalId, purchaseOrder.OrderId, SumPrice));
                    var symmetric = SymmetricAlgorithm.Create("TripleDes");
                    symmetric.Mode = CipherMode.ECB;
                    symmetric.Padding = PaddingMode.PKCS7;
                    var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(terminalKey), new byte[8]);
                    var signData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

                    var ipgUri = "https://sadad.shaparak.ir/api/v0/Request/PaymentRequest";
                    var data = new
                    {
                        MerchantId = merchantId,
                        TerminalId = terminalId,
                        Amount = SumPrice,
                        purchaseOrder.OrderId,
                        LocalDateTime = DateTime.Now,
                        ReturnUrl = url + returnAction,
                        signData
                    };

                    var res = await CallApi<PayResultData>(ipgUri, data);

                    if (res.ResCode == "0")
                    {
                        await _purchaseOrderService.Edit(purchaseOrder);
                        return Redirect($"https://sadad.shaparak.ir/Purchase?Token={res.Token}");
                    }
                    return RedirectToPage("Error", new { message = res.Description });
            }
            Code = "Error";
            Message = "خطا هنگام اتصال به درگاه بانکی";
        }
        else
        {
            if (string.IsNullOrEmpty(Message) || Message.Equals("\n\r"))
            {
                Message = "لطفا اطلاعات آدرس را تکمیل کنید یا از لیست یک آدرس را انتخاب کنید";
                Code = "Info";
            }
        }
        Code = resultSendInformation.ToString();
        return Page();
    }

    public static async Task<T> CallApi<T>(string apiUrl, object value)
    {
        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
        using (var client = new HttpClient())
        {

            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, value);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            return default(T);
        }
    }
}