using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using Ecommerce.Entities.HolooEntity;

namespace ECommerce.API.Repository;

public class HolooUnitRepository : HolooRepository<HolooUnit>, IHolooUnitRepository
{
    private readonly HolooDbContext _context;

    public HolooUnitRepository(HolooDbContext context) : base(context)
    {
        _context = context;
    }
}