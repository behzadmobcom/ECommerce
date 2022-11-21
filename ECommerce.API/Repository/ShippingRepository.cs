using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using Ecommerce.Entities;

namespace ECommerce.API.Repository;

public class ShippingRepository : AsyncRepository<Shipping>, IShippingRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public ShippingRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }
}