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
        Task<ServiceResult<List<PurchaseListViewModel>>> PurchaseList(string search = "",
         int pageNumber = 0, int pageSize = 10, int purchaseSort = 1, bool? isPaied = null);
    }
}
