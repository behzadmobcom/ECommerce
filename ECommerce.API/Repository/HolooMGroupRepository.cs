using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using Ecommerce.Entities.HolooEntity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repository;

public class HolooMGroupRepository : HolooRepository<HolooMGroup>, IHolooMGroupRepository
{
    private readonly HolooDbContext _context;

    public HolooMGroupRepository(HolooDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<HolooMGroup> GetByCode(string code, CancellationToken cancellationToken)
    {
        return await _context.M_GROUP.Where(x => x.M_groupcode == code).FirstAsync(cancellationToken);
    }
}