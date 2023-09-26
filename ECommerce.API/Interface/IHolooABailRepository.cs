using Ecommerce.Entities.HolooEntity;

namespace ECommerce.API.Interface;

public interface IHolooABailRepository : IHolooRepository<HolooABail>
{
    Task<bool> Add(List<HolooABail> aBails, CancellationToken cancellationToken);

    Task<List<HolooABail>> GetAll(CancellationToken cancellationToken);

    double GetWithACode(int userCode, string aCode, CancellationToken cancellationToken);
}