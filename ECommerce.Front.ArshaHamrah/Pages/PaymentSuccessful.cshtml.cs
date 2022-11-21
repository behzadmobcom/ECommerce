using Ecommerce.Entities;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZarinpalSandbox;

namespace ECommerce.Front.ArshaHamrah.Pages;

public class PaymentSuccessfulModel : PageModel
{
    private readonly IPurchaseOrderService _purchaseOrderService;
    private readonly ICartService _cartService;

    public string Refid { get; set; }
    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }
    public PurchaseOrder PurchaseOrder { get; set; }
    public List<PurchaseOrderViewModel> CartList { get; set; }


    public PaymentSuccessfulModel(IPurchaseOrderService purchaseOrderService, ICartService cartService)
    {
        _purchaseOrderService = purchaseOrderService;
        _cartService = cartService;
    }

    public async Task<ActionResult> OnGet(string factor, string status, string authority)
    {
        if (string.IsNullOrEmpty(status) == false && string.IsNullOrEmpty(authority) == false &&
            string.IsNullOrEmpty(factor) == false && status.ToLower() == "ok")
        {
            var resultOrder = await _purchaseOrderService.GetByUserId();
            PurchaseOrder = resultOrder.ReturnData;
            var amount =Convert.ToInt32(PurchaseOrder.Amount);
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
                    CartList = (await _cartService.CartListFromServer()).ReturnData;

                    return Page();
            }
        }
        return RedirectToPage("Error", new { message = "مشکل در درگاه پرداخت" });
    }
     
}