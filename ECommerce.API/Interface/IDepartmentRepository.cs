using System.Threading;
using Entities;
using System.Threading.Tasks;
using API.Utilities;
using Entities.Helper;

namespace API.Interface
{
    public interface IDepartmentRepository : IAsyncRepository<Department>
    {
        Task<PagedList<Department>> Search(PaginationParameters paginationParameters,
            CancellationToken cancellationToken);
        Task<Department> GetByTitle(string name, CancellationToken cancellationToken);
    }
}
