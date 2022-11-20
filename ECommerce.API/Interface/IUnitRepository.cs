using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.API.Interface;

public interface IUnitRepository : IAsyncRepository<Unit>
{
    Task<PagedList<Unit>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    Task<Unit> GetByName(string name, CancellationToken cancellationToken);

    Task<int> AddAll(IEnumerable<Unit> units, CancellationToken cancellationToken);

    int? GetId(int? unitCode, CancellationToken cancellationToken);
}