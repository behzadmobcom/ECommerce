using Ecommerce.Entities;

namespace ECommerce.API.Interface;

public interface ICityRepository : IAsyncRepository<City>
{
    Task<City> GetByName(string name, CancellationToken cancellationToken);
}