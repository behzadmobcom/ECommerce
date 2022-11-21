using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.API.Interface;

public interface IBrandRepository : IAsyncRepository<Brand>
{
    Task<PagedList<Brand>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    Task<Brand> GetByName(string name, CancellationToken cancellationToken);
}