using Ecommerce.Entities.HolooEntity;

namespace ECommerce.API.Interface
{
    public interface IHolooSanadRepository : IHolooRepository<HolooSanad>
    {
        Task<(string, string)> Add(HolooSanad sanad, CancellationToken cancellationToken);
    }
}
