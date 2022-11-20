using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.API.Interface;

public interface IDepartmentRepository : IAsyncRepository<Department>
{
    Task<PagedList<Department>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken);

    Task<Department> GetByTitle(string name, CancellationToken cancellationToken);
}