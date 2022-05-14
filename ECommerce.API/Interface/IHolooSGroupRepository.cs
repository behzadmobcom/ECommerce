using Entities.HolooEntity;

namespace API.Interface;

public interface IHolooSGroupRepository : IHolooRepository<HolooSGroup>
{
    Task<IEnumerable<HolooSGroup>> GetSGroupByMCode(string mCode, CancellationToken cancellationToken);
}