using System.Threading;
using Entities;
using System.Threading.Tasks;

namespace API.Interface
{
    public interface ICityRepository : IAsyncRepository<City>
    {
        Task<City> GetByName(string name, CancellationToken cancellationToken);
    }
}
