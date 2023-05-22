using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Pages
{
    public class InvoiceReportPrintModel : PageModel
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        public string SystemTraceNo { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }

        public InvoiceReportPrintModel(IPurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }

        public async Task OnGet(long orderId)
        {
            //SystemTraceNo = systemTraceNo;
            var result = await _purchaseOrderService.GetByUserAndOrderId(orderId);
            PurchaseOrder = result.ReturnData;
        }
    }
}
