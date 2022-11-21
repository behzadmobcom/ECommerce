using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.API.Interface;

public interface ISupplierRepository : IAsyncRepository<Supplier>
{
    Task<PagedList<Supplier>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken);

    Task<Supplier> GetByName(string name, CancellationToken cancellationToken);
}