using API.Utilities;
using Entities;
using Entities.Helper;
using Entities.ViewModel;

namespace API.Interface;

public interface IEmployeeRepository : IAsyncRepository<Employee>
{
    Task<PagedList<Employee>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);

    Task<Employee> GetByName(string name, CancellationToken cancellationToken);

    
}