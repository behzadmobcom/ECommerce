using API.Utilities;
using Entities;
using Entities.Helper;

namespace API.Interface;

public interface IDepartmentRepository : IAsyncRepository<Department>
{
    Task<PagedList<Department>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken);

    Task<Department> GetByTitle(string name, CancellationToken cancellationToken);
}