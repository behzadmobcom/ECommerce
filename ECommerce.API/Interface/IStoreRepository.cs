using System.Threading;
using Entities;
using System.Threading.Tasks;
using API.Utilities;
using Entities.Helper;

namespace API.Interface
{
    public interface IStoreRepository : IAsyncRepository<Store>
    {
        Task<PagedList<Store>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
        Task<Store> GetByName(string name, CancellationToken cancellationToken);
    }
}
