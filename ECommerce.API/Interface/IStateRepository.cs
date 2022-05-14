using Entities;

namespace API.Interface;

public interface IStateRepository : IAsyncRepository<State>
{
    Task<State> GetByName(string name, CancellationToken cancellationToken);
}