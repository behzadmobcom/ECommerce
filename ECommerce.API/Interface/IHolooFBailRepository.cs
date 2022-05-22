using Entities.HolooEntity;

namespace API.Interface;

public interface IHolooFBailRepository :  IHolooRepository<HolooFBail>
{
    Task<string> Add(HolooFBail bail, CancellationToken cancellationToken);
    Task<(string fCode, int fCodeC)> GetFactorCode(CancellationToken cancellationToken);
}