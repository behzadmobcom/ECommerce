using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.API.Interface;

public interface IColorRepository : IAsyncRepository<Color>
{
    Task<PagedList<Color>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    Task<Color> GetByName(string name, CancellationToken cancellationToken);
}