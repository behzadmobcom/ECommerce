using API.Interface;
using Ecommerce.Entities.HolooEntity;

namespace ECommerce.API.Interface
{
    public interface IHolooSanadListRepository : IHolooRepository<HolooSndList>
    {
        Task<bool> Add(HolooSndList sanadList, CancellationToken cancellationToken);
    }
}
