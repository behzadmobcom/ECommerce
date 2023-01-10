using Ecommerce.Entities;
using Ecommerce.Entities.ViewModel;
using ECommerce.Front.BolouriGroup.Models;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;

namespace ECommerce.Front.BolouriGroup.Pages;


public class InvoiceModel : PageModel
{
    private readonly IPurchaseOrderService _purchaseOrderService;
    private readonly ICartService _cartService;

    public string Refid { get; set; }
    public string SystemTraceNo { get; set; }
    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }
    public PurchaseOrder PurchaseOrder { get; set; }

    public InvoiceModel(IPurchaseOrderService purchaseOrderService, ICartService cartService)
    {
        _purchaseOrderService = purchaseOrderService;
        _cartService = cartService;
    }

    public async Task<ActionResult> OnGet(PurchaseResult result)
    {
        return await pay(result);
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
        var merchantKey = "8v8AEee8YfZX+wwc1TzfShRgH3O9WOho";// "CSlQf8zTne2YH3mnrbwAnKx3rl9ckHKz";
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

                return Page();
            }
        }
        return RedirectToPage("Error", new { message = "مشکل در درگاه پرداخت " + res.Result.Description });
    }

    public static async Task<T> CallApi<T>(string apiUrl, object value)
    {
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