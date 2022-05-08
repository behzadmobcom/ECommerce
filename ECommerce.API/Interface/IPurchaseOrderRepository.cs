using Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Utilities;
using Entities.Helper;
using Entities.ViewModel;

namespace API.Interface
{
    public interface IPurchaseOrderRepository : IAsyncRepository<PurchaseOrder>
    {
        Task<PagedList<PurchaseOrder>> Search(PaginationParameters paginationParameters,
            CancellationToken cancellationToken);
        Task<PurchaseOrder> GetByUser(int id, CancellationToken cancellationToken);

        Task<IEnumerable<PurchaseOrderViewModel>> GetProductListByUserId(int userId, CancellationToken cancellationToken);
    }
}
