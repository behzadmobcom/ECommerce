using Ecommerce.Entities;
using Ecommerce.Entities.ViewModel;
using ECommerce.Front.ArshaHamrah.Utilities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceReferenceMellat;

namespace ECommerce.Front.ArshaHamrah.Pages;


public class PaymentSuccessfulMellatModel : PageModel
{
    private readonly IPurchaseOrderService _purchaseOrderService;
    private readonly ICartService _cartService;

    public string Refid { get; set; }
    public string Message { get; set; }
    public string SaleReference { get; set; }

    [TempData] public string Code { get; set; }
    public PurchaseOrder PurchaseOrder { get; set; }
    public List<PurchaseOrderViewModel> CartList { get; set; }


    public PaymentSuccessfulMellatModel(IPurchaseOrderService purchaseOrderService, ICartService cartService)
    {
        _purchaseOrderService = purchaseOrderService;
        _cartService = cartService;
    }

    //string RefId, string ResCode, long SaleOrderId,long SaleReferenceId, string PanCardHolder, string CreditCardSaleResponseDetail,long FinalAmount

    public async Task<ActionResult> OnGet(string RefId, string ResCode, long SaleOrderId, long SaleReferenceId, string PanCardHolder, string CreditCardSaleResponseDetail, long FinalAmount)
    {
        if (string.IsNullOrEmpty(SaleOrderId.ToString()))
        {
            //ResCode=StatusPayment
            if (!string.IsNullOrEmpty(ResCode))
            {
                Message = MellatErrorCode.GetMessage(ResCode);
                SaleReference = "**************";

            }
            else
            {
                Message = "رسید قابل قبول نیست";
                SaleReference = "**************";

            }
        }
        else
        {
            if (!string.IsNullOrEmpty(RefId))
            {
                var resultOrder = await _purchaseOrderService.GetByUserId();
                PurchaseOrder = resultOrder.ReturnData;
                var amount = Convert.ToInt32(PurchaseOrder.Amount);
                var paymentMellat = new PaymentGatewayClient();
                long terminalId = 6547305;
                var userName = "Arshahamrah110";
                var password = "79916117";
                var resultBody = await paymentMellat.bpVerifyRequestAsync(
                    terminalId,
                    userName,
                    password,
                    PurchaseOrder.Id,
                    SaleOrderId,
                    SaleReferenceId);
                //if (FinalAmount != PurchaseOrder.Amount)
                //    Message = "مبلغ واریزی با مبلغ فاکتور همخوانی ندارد";

                var resultVerify = resultBody.Body.@return;
                if (!string.IsNullOrEmpty(resultVerify))
                {
                    if (resultVerify == "0")
                    {
                        var IQresult = await paymentMellat.bpInquiryRequestAsync(terminalId, userName, password, SaleOrderId, SaleOrderId, SaleReferenceId);
                        var resultIQresult = IQresult.Body.@return;
                        if (resultIQresult == "0")
                        {
                            var Sresult = await paymentMellat.bpSettleRequestAsync(terminalId, userName, password, SaleOrderId, SaleOrderId, SaleReferenceId);

                            if (Sresult != null)
                            {
                                if (Sresult.Body.@return == "0" || Sresult.Body.@return == "45")
                                {
                                    PurchaseOrder.Transaction.RefId = RefId;
                                    PurchaseOrder.Transaction.PaymentId = Convert.ToInt64(SaleOrderId);
                                    PurchaseOrder.Transaction.Amount = amount;
                                    var result = await _purchaseOrderService.Pay(PurchaseOrder);
                                    Message = result.Message;
                                    Code = result.Code.ToString();
                                    CartList = (await _cartService.CartListFromServer()).ReturnData;

                                }

                                return Page();
                            }
                        }
                        PurchaseOrder.Transaction = new();

                    }
                }
            }
        }
        return RedirectToPage("Error", new { message = "مشکل در درگاه پرداخت" });
    }
}