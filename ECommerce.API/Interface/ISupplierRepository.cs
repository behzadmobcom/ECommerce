using System.Threading;
using Entities;
using System.Threading.Tasks;
using API.Utilities;
using Entities.Helper;

namespace API.Interface
{
    public interface ISupplierRepository : IAsyncRepository<Supplier>
    {
        Task<PagedList<Supplier>> Search(PaginationParameters paginationParameters,
            CancellationToken cancellationToken);
        Task<Supplier> GetByName(string name, CancellationToken cancellationToken);
    }
}
