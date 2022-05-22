using API.DataContext;
using API.Repository;
using Ecommerce.Entities.HolooEntity;
using ECommerce.API.Interface;

namespace ECommerce.API.Repository
{
    public class HolooCustomerRepository : HolooRepository<HolooCustomer>, IHolooCustomerRepository
    {
        private readonly HolooDbContext _context;

        public HolooCustomerRepository(HolooDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<string> Add(HolooCustomer customer, CancellationToken cancellationToken)
        {
            await _context.Customer
          await _context.SaveChangesAsync();
        }
    }
}
