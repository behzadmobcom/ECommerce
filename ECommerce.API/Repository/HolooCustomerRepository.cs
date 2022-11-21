using ECommerce.API.DataContext;
using Ecommerce.Entities.HolooEntity;
using ECommerce.API.Interface;
using Microsoft.EntityFrameworkCore;

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
            await _context.Customer.AddAsync(customer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return customer.C_Code;
        }

        public async Task<string> GetNewCustomerCode()
        {
            var customer =await _context.Customer.OrderByDescending(x => x.C_Code).FirstOrDefaultAsync();
            var customerCode = customer == null ? "00000" : customer.C_Code;
            return (Convert.ToInt32(customerCode) + 1).ToString("D5");
        }
        public async Task<HolooCustomer> GetCustomerByCode(string customerCode)
        {
            return await _context.Customer.FirstAsync(x => x.C_Code== customerCode);
        }
    }
}
