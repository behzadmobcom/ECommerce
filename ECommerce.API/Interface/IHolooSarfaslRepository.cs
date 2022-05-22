using API.Interface;
using Ecommerce.Entities.HolooEntity;

namespace ECommerce.API.Interface
{
    public interface IHolooSarfaslRepository : IHolooRepository<HolooSarfasl>
    {
        Task<string> Add(HolooSarfasl sarfasl, CancellationToken cancellationToken);
    }
}
