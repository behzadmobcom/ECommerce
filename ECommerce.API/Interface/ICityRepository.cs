using Ecommerce.Entities;
using ECommerce.API.Utilities;
using Ecommerce.Entities.Helper;
using System.Threading.Tasks;

namespace ECommerce.API.Interface;

public interface ICityRepository : IAsyncRepository<City>
{
    Task<City> GetByName(string name, CancellationToken cancellationToken);

    Task<PagedList<City>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
}