using System.Threading;
using System.Threading.Tasks;
using API.Utilities;
using Entities;
using Entities.Helper;

namespace API.Interface
{
    public interface ICurrencyRepository : IAsyncRepository<Currency>
    {
        Task<PagedList<Currency>> Search(PaginationParameters paginationParameters,
            CancellationToken cancellationToken);
        Task<Currency> GetByName(string name, CancellationToken cancellationToken);
    }
}
