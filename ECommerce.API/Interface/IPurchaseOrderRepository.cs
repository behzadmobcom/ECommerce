using API.Utilities;
using Entities;
using Entities.Helper;
using Entities.ViewModel;

namespace API.Interface;

public interface IPurchaseOrderRepository : IAsyncRepository<PurchaseOrder>
{
    Task<PagedList<PurchaseOrder>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken);

    Task<PurchaseOrder?> GetByUser(int id, CancellationToken cancellationToken);

    Task<IEnumerable<PurchaseOrderViewModel>> GetProductListByUserId(int userId, CancellationToken cancellationToken);
}