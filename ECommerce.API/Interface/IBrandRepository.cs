using API.Utilities;
using Entities;
using Entities.Helper;

namespace API.Interface;

public interface IBrandRepository : IAsyncRepository<Brand>
{
    Task<PagedList<Brand>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    Task<Brand> GetByName(string name, CancellationToken cancellationToken);
}