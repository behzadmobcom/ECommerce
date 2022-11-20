using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.API.Interface;

public interface IStoreRepository : IAsyncRepository<Store>
{
    Task<PagedList<Store>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    Task<Store> GetByName(string name, CancellationToken cancellationToken);
}