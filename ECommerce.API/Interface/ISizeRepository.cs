using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.API.Interface;

public interface ISizeRepository : IAsyncRepository<Size>
{
    Task<PagedList<Size>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    Task<Size> GetByName(string name, CancellationToken cancellationToken);
}