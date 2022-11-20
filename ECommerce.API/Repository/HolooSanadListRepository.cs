using ECommerce.API.DataContext;
using Ecommerce.Entities.HolooEntity;
using ECommerce.API.Interface;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repository
{
    public class HolooSanadListRepository : HolooRepository<HolooSndList>, IHolooSanadListRepository
    {
        private readonly HolooDbContext _context;

        public HolooSanadListRepository(HolooDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> Add(HolooSndList sanadList, CancellationToken cancellationToken)
        {
           // _context.Entry(sanadList).State = EntityState.Detached;
            await _context.Snd_List.AddAsync(sanadList, cancellationToken);
           var result= await _context.SaveChangesAsync(cancellationToken);
            return result == 0;
        }
    }
}
