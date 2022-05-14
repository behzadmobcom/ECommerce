using API.Utilities;
using Entities;
using Entities.Helper;

namespace API.Interface;

public interface ISizeRepository : IAsyncRepository<Size>
{
    Task<PagedList<Size>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    Task<Size> GetByName(string name, CancellationToken cancellationToken);
}