using API.DataContext;
using API.Interface;
using Entities;

namespace API.Repository
{
    public class ShippingRepository : AsyncRepository<Shipping>, IShippingRepository
    {
        private readonly SunflowerECommerceDbContext _context;
        public ShippingRepository(SunflowerECommerceDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
