using Ecommerce.Entities.HolooEntity;

namespace ECommerce.API.Interface;

public interface IHolooMGroupRepository : IHolooRepository<HolooMGroup>
{
    Task<HolooMGroup> GetByCode(string code, CancellationToken cancellationToken);
}