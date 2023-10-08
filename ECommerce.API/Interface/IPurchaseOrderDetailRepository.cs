using Ecommerce.Entities;
using Ecommerce.Entities.ViewModel;

namespace ECommerce.API.Interface;

public interface IPurchaseOrderDetailRepository : IAsyncRepository<PurchaseOrderDetail>
{
    Task<List<PurchaseOrderDetail>> GetByPurchaseOrderId(int id, CancellationToken cancellationToken);
    Task UpdateUserCart(IEnumerable<PurchaseOrderViewModel> purchaseOrderList, CancellationToken cancellationToken);
}