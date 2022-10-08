using Entities.HolooEntity;

namespace API.Interface;

public interface IHolooABailRepository : IHolooRepository<HolooABail>
{
    Task<bool> Add(List<HolooABail> aBails, CancellationToken cancellationToken);

    Task<List<HolooABail>> GetAll(CancellationToken cancellationToken);
}