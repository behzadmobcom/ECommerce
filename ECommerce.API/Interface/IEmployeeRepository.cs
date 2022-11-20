using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.API.Interface;

public interface IEmployeeRepository : IAsyncRepository<Employee>
{
    Task<PagedList<Employee>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);

    Task<Employee> GetByName(string name, CancellationToken cancellationToken);

    
}