using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.API.Interface;

public interface ICurrencyRepository : IAsyncRepository<Currency>
{
    Task<PagedList<Currency>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken);

    Task<Currency> GetByName(string name, CancellationToken cancellationToken);
}