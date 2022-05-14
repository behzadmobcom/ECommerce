using API.Utilities;
using Entities;
using Entities.Helper;

namespace API.Interface;

public interface IStoreRepository : IAsyncRepository<Store>
{
    Task<PagedList<Store>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    Task<Store> GetByName(string name, CancellationToken cancellationToken);
}