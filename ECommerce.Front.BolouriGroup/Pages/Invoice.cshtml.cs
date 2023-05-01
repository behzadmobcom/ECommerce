using Ecommerce.Entities;
using ECommerce.Front.BolouriGroup.Models;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;
using PersianDate.Standard;

namespace ECommerce.Front.BolouriGroup.Pages;


public class InvoiceModel : PageModel
{
    public long orderId=0;
    private readonly IUserService _userService;
    private readonly IPurchaseOrderService _purchaseOrderService;

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

    public async Task<IActionResult> Print()
    {
        return RedirectToPage("InvoiceReportPrint", new
        {
            systemTraceNo = SystemTraceNo,
        });
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
                    Amount = amount
                };
                var resulPay = await _purchaseOrderService.Pay(PurchaseOrder);
                Message = resulPay.Message;
                Code = resulPay.Code.ToString();

                string message =
                    $"پیش فاکتور به شماره {PurchaseOrder.OrderId} به شماره پیگیری {res.Result.RetrivalRefNo} به مبلغ {PurchaseOrder.Amount} در تاریخ {PurchaseOrder.CreationDate.ToFa("f")} صادر شد";
                await _userService.SendAuthenticationSms("09118876347", message);
                await _userService.SendAuthenticationSms("09909052454", message);
                await _userService.SendAuthenticationSms("09119384108", message);
                
                orderId = PurchaseOrder.OrderId;
                return Page();
            }
        }
        return RedirectToPage("Error", new { message = "مشکل در درگاه پرداخت " + res.Result.Description });
    }

    public async Task<IActionResult> OnGetPrint()
    {
        var purchase_Order = await _purchaseOrderService.GetByUserAndOrderId(orderId);
        if (purchase_Order != null)
        {
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            purchaseOrder.OrderGuid = Guid.NewGuid();
            purchaseOrder.CreationDate = purchase_Order.ReturnData.CreationDate;
            purchaseOrder.Amount = purchase_Order.ReturnData.Amount;
            foreach (var item in purchase_Order.ReturnData.PurchaseOrderDetails)
            {
                purchaseOrder.PurchaseOrderDetails.Add(item);
            }
            purchaseOrder.SendInformation = purchase_Order.ReturnData.SendInformation;
            purchaseOrder.Description = purchase_Order.ReturnData.Description;

            return RedirectToPage("InvoiceReportPrint", new
            {
                purchaseOrder = purchaseOrder,
                systemTraceNo = "systemTraceNo",
                refid = "refid"
            });
        }
        else
            return RedirectToPage("Error", new { message = "فاکتور موجود نمی باشد" });
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