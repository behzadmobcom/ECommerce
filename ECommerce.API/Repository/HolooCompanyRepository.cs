using API.DataContext;
using API.Interface;
using Entities;

namespace API.Repository
{
    public class HolooCompanyRepository : AsyncRepository<HolooCompany>, IHolooCompanyRepository
    {
        private readonly SunflowerECommerceDbContext _context;
        public HolooCompanyRepository(SunflowerECommerceDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
