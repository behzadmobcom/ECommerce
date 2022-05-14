using API.DataContext;
using API.Interface;
using Entities.HolooEntity;

namespace API.Repository;

public class HolooUnitRepository : HolooRepository<HolooUnit>, IHolooUnitRepository
{
    private readonly HolooDbContext _context;

    public HolooUnitRepository(HolooDbContext context) : base(context)
    {
        _context = context;
    }
}