using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using Ecommerce.Entities;

namespace ECommerce.API.Repository;

public class HolooCompanyRepository : AsyncRepository<HolooCompany>, IHolooCompanyRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public HolooCompanyRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }
}