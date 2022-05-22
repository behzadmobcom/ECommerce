using API.Interface;
using Ecommerce.Entities.HolooEntity;

namespace ECommerce.API.Interface
{
    public interface IHolooSanadRepository : IHolooRepository<HolooSanad>
    {
        Task<string> Add(HolooSanad sanad, CancellationToken cancellationToken);
    }
}
