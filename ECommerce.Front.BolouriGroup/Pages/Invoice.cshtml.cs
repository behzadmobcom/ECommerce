using Ecommerce.Entities;
using ECommerce.Front.BolouriGroup.Models;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;
using PersianDate.Standard;
using ZarinpalSandbox;

namespace ECommerce.Front.BolouriGroup.Pages;


public class InvoiceModel : PageModel
{
    private readonly IUserService _userService;
    private readonly IPurchaseOrderService _purchaseOrderService;
    [BindProperty] public long OrderId { get; set; }
    public string Refid { get; set; }
    public string SystemTraceNo { get; set; }
    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }
    public PurchaseOrder PurchaseOrder { get; set; }

    public InvoiceModel(IPurchaseOrderService purchaseOrderService, IUserService userService)
    {
        _purchaseOrderService = purchaseOrderService;
        _userService = userService;
    }

    public async Task<ActionResult> OnGet(PurchaseResult result)
    {
        return await pay(result);
    }

    public async Task<ActionResult> OnGetPayZarinpal(string factor, string status, string authority)
    {
        if (string.IsNullOrEmpty(status) == false && string.IsNullOrEmpty(authority) == false &&
            string.IsNullOrEmpty(factor) == false && status.ToLower() == "ok")
        {
            var resultOrder = await _purchaseOrderService.GetByUserId();
            PurchaseOrder = resultOrder.ReturnData;
            var amount = Convert.ToInt32(PurchaseOrder.Amount);
            var statusInt = await new Payment(amount).Verification(authority);
            switch (statusInt.Status)
            {
                case -1:
                    Message = "اطلاعات ارسال شده ناقص است.";
                    break;
                case -2:
                    Message = "مشکل نامشخص در درگاه پرداخت با کد " + statusInt.Status.ToString();
                    break;
                case -11:
                    Message = "درخواست مورد نظر یافت نشد.";
                    break;
                case -22:
                    Message = "تراکنش ناموفق می باشد.";
                    break;
                case -33:
                    Message = "مبلغ تراکنش با مبلغ پرداخت شده مطابقت ندارد.";
                    break;
                case 100:
                case 101:
                    //Success

                    Refid = statusInt.RefId.ToString();
                    PurchaseOrder.Transaction = new();
                    PurchaseOrder.Transaction.RefId = Refid;
                    PurchaseOrder.Transaction.Amount = amount;
                    var result = await _purchaseOrderService.Pay(PurchaseOrder);
                    Message = result.Message;
                    Code = result.Code.ToString();
                    //CartList = (await _cartService.CartListFromServer()).ReturnData;
                    if (result.Code == 0 && result.Message != null)
                    {
                        await _userService.SendInvocieSms(result.Message, "09111307006", PurchaseOrder.CreationDate.ToFa());
                    }
                    OrderId = PurchaseOrder.OrderId;
                    return Page();
            }
        }
        return RedirectToPage("Error", new { message = "مشکل در درگاه پرداخت" });
    }

    private async Task<ActionResult> pay(PurchaseResult result)
    {
        if (!result.ResCode.Equals("0"))
        {
            switch (result.ResCode)
            {
                case "100":
                    Message = "درخواست تکراری است. قبلا در سیستم ثبت شده است";
                    break;
                case "-1":
                    Message = "پارامترهای ارسالی صحیح نمی باشد یا تراکنش در سیستم وجود ندارد";
                    break;
                case "101":
                    Message = "مهلت ارسال تراکنش به پایان رسیده است";
                    break;
            }
            Code = "Error";
            return Page();
        }
        var dataBytes = Encoding.UTF8.GetBytes(result.Token);
        var symmetric = SymmetricAlgorithm.Create("TripleDes");
        symmetric.Mode = CipherMode.ECB;
        symmetric.Padding = PaddingMode.PKCS7;
        var merchantKey = "CSlQf8zTne2YH3mnrbwAnKx3rl9ckHKz";//"8v8AEee8YfZX+wwc1TzfShRgH3O9WOho";// "CSlQf8zTne2YH3mnrbwAnKx3rl9ckHKz";
        var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(merchantKey), new byte[8]);

        var signedData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

        var data = new
        {
            token = result.Token,
            SignData = signedData
        };

        var resultOrder = await _purchaseOrderService.GetByUserId();
        PurchaseOrder = resultOrder.ReturnData;
        PurchaseOrder.Amount *= 10;
        var amount = Convert.ToInt32(PurchaseOrder.Amount);
        var ipgUri = "https://sadad.shaparak.ir/api/v0/Advice/Verify";

        var res = CallApi<VerifyResultData>(ipgUri, data);
        if (res != null && res.Result != null)
        {
            if (res.Result.ResCode == "0")
            {
                result.VerifyResultData = res.Result;
                res.Result.Succeed = true;
                SystemTraceNo = res.Result.SystemTraceNo;
                PurchaseOrder.Transaction = new()
                {
                    RefId = res.Result.RetrivalRefNo,
                    Amount = amount,
                    UserId = resultOrder.ReturnData.UserId
                };
                var resulPay = await _purchaseOrderService.Pay(PurchaseOrder);
                Message = resulPay.Message;
                Code = resulPay.Code.ToString();

                if (resulPay.Code == 0 && resulPay.Message != null)
                {
                    await _userService.SendInvocieSms(resulPay.Message, "09118876347", PurchaseOrder.CreationDate.ToFa());
                    await _userService.SendInvocieSms(resulPay.Message, "09909052454", PurchaseOrder.CreationDate.ToFa());
                    await _userService.SendInvocieSms(resulPay.Message, "09119384108", PurchaseOrder.CreationDate.ToFa());
                    await _userService.SendInvocieSms(resulPay.Message, "09111307006", PurchaseOrder.CreationDate.ToFa());
                }

                OrderId = PurchaseOrder.OrderId;
                return Page();
            }
        }
        return RedirectToPage("Error", new { message = "مشکل در درگاه پرداخت " + res.Result.Description });
    }
    public IActionResult OnGetFactorPrint(long orderId)
    {
        return RedirectToPage("InvoiceReportPrint", new { orderId = orderId });
    }
    public static async Task<T> CallApi<T>(string apiUrl, object value)
    {
        using var client = new HttpClient();
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