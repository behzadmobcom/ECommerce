using Entities;

namespace API.Interface;

public interface IPurchaseOrderDetailRepository : IAsyncRepository<PurchaseOrderDetail>
{
    Task<List<PurchaseOrderDetail>> GetByPurchaseOrderId(int id, CancellationToken cancellationToken);
}