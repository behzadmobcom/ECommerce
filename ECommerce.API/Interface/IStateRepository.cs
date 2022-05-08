using System.Threading;
using Entities;
using System.Threading.Tasks;

namespace API.Interface
{
    public interface IStateRepository : IAsyncRepository<State>
    {
        Task<State> GetByName(string name, CancellationToken cancellationToken);
    }
}
