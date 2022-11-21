using Ecommerce.Entities.HolooEntity;

namespace ECommerce.API.Interface
{
    public interface IHolooSarfaslRepository : IHolooRepository<HolooSarfasl>
    {
        Task<string> Add(string username, CancellationToken cancellationToken);
    }
}
