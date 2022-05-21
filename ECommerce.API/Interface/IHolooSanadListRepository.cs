using API.Interface;
using Ecommerce.Entities.HolooEntity;

namespace ECommerce.API.Interface
{
    public interface IHolooSanadListRepository : IHolooRepository<HolooSndList>
    {
        Task<bool> AddRang(IEnumerable<HolooSndList> sanadLists, CancellationToken cancellationToken);
    }
}
