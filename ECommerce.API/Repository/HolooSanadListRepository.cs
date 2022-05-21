using API.DataContext;
using API.Repository;
using Ecommerce.Entities.HolooEntity;
using ECommerce.API.Interface;

namespace ECommerce.API.Repository
{
    public class HolooSanadListRepository : HolooRepository<HolooSndList>, IHolooSanadListRepository
    {
        private readonly HolooDbContext _context;

        public HolooSanadListRepository(HolooDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddRang(IEnumerable<HolooSndList> sanadLists, CancellationToken cancellationToken)
        {
           await _context.SanadList.AddRangeAsync(sanadLists, cancellationToken);
           var result= await _context.SaveChangesAsync();
            return result == 0;
        }
    }
}
