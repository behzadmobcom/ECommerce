using Entities;

namespace API.Interface;

public interface ICityRepository : IAsyncRepository<City>
{
    Task<City> GetByName(string name, CancellationToken cancellationToken);
}