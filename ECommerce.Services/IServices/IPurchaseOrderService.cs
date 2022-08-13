using Entities;
using Entities.Helper;
using Entities.ViewModel;

namespace ECommerce.Services.IServices
{
    public interface IPurchaseOrderService: IEntityService<PurchaseOrder>
    {
        Task<ServiceResult> Pay(PurchaseOrder purchaseOrder);
        Task<ServiceResult> Add(PurchaseOrder purchaseOrder);
        Task<ServiceResult> Edit(PurchaseOrder purchaseOrder);
        Task<ServiceResult<PurchaseOrder>> GetByUserId();
        Task<ServiceResult<PurchaseOrder>> GetByOrderId(long orderId);
        Task<ServiceResult<List<PurchaseListViewModel>>> PurchaseList(int userId = 0, string search = "",
         int pageNumber = 0, int pageSize = 10, int purchaseSort = 1, bool? isPaied = null, DateTime? fromCreationDate = null,
         DateTime? toCreationDate = null, int? statusId = null, decimal? minimumAmount = null, decimal? maximumAmount = null,
         PaymentMethodStatus? paymentMethodStatus = null);
    }
}
