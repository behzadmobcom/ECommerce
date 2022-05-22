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

        public Task<string> Add(HolooCustomer customer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
