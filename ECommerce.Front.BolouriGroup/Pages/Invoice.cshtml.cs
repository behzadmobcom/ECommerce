using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZarinpalSandbox;

namespace Bolouri.Pages;

public class InvoiceModel : PageModel
{
    private readonly IPurchaseOrderService _purchaseOrderService;

    public string Refid { get; set; }
    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public InvoiceModel(IPurchaseOrderService purchaseOrderService)
    {
        _purchaseOrderService = purchaseOrderService;
    }


    public async Task<ActionResult> OnGet(string factor, string status, string authority)
    {
        if (string.IsNullOrEmpty(status) == false && string.IsNullOrEmpty(authority) == false &&
            string.IsNullOrEmpty(factor) == false && status.ToLower() == "ok")
        {
            var resultOrder = await _purchaseOrderService.GetByUserId();
            var order = resultOrder.ReturnData;
            var amount = order.Amount;
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
                    order.Transaction = new();
                    order.Transaction.RefId = Refid;
                    order.Transaction.Amount = amount;
                    var result = await _purchaseOrderService.Pay(order);
                    Message = result.Message;
                    Code = result.Code.ToString();

                    return Page();
            }
        }

        return RedirectToPage("Error", new { message = "مشکل در درگاه پرداخت" });
    }
}