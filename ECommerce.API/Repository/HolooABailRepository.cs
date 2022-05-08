using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.DataContext;
using API.Interface;
using Entities.HolooEntity;

namespace API.Repository
{
    public class HolooABailRepository : HolooRepository<HolooABail>, IHolooABailRepository
    {
        private readonly HolooDbContext _context;
        public HolooABailRepository(HolooDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> Add(List<HolooABail> aBails, CancellationToken cancellationToken)
        {
           await _context.AddRangeAsync(aBails,cancellationToken);
           var result = await _context.SaveChangesAsync(cancellationToken);
           return result > 0;
        }
    }
}
