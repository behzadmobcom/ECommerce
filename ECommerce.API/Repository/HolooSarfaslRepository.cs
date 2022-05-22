using API.DataContext;
using API.Repository;
using Ecommerce.Entities.HolooEntity;
using ECommerce.API.Interface;

namespace ECommerce.API.Repository
{
    public class HolooSarfaslRepository : HolooRepository<HolooSarfasl>, IHolooSarfaslRepository
    {
        private readonly HolooDbContext _context;

        public HolooSarfaslRepository(HolooDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<string> Add(HolooSarfasl sarfasl, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
