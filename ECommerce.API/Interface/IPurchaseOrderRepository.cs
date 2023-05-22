using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.ViewModel;

namespace ECommerce.API.Interface;

public interface IPurchaseOrderRepository : IAsyncRepository<PurchaseOrder>
{
    Task<PagedList<PurchaseListViewModel>> Search(PurchaseFiltreOrderViewModel purchaseFiltreOrderViewModel,
        CancellationToken cancellationToken);

    Task<PurchaseOrder?> GetByUser(int id, CancellationToken cancellationToken);

    Task<PurchaseOrder?> GetByOrderId(long id, CancellationToken cancellationToken);

    Task<IEnumerable<PurchaseOrderViewModel>> GetProductListByUserId(int userId, CancellationToken cancellationToken);

    Task<PurchaseOrder> GetByOrderIdWithInclude(long orderId, CancellationToken cancellationToken);
    Task<PurchaseOrder> GetPurchaseOrderWithIncludeById(int id, CancellationToken cancellationToken);
    Task<PurchaseOrder> GetByUserAndOrderId(int userId, long orderId, CancellationToken cancellationToken);
}