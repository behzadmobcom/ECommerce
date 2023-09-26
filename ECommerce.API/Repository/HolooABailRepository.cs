using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using Ecommerce.Entities.HolooEntity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repository;

public class HolooABailRepository : HolooRepository<HolooABail>, IHolooABailRepository
{
    private readonly HolooDbContext _context;

    public HolooABailRepository(HolooDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> Add(List<HolooABail> aBails, CancellationToken cancellationToken)
    {
        await _context.AddRangeAsync(aBails, cancellationToken);
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<List<HolooABail>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.ABAILPRE.ToListAsync(cancellationToken);
    }

    public double GetWithACode(int userCode, string aCode, CancellationToken cancellationToken)
    {
        var result = (from d in _context.ABAILPRE.Where(c => c.A_Code == aCode)
                            join dr in _context.FBAILPRE.Where(c => c.UserCode == userCode) on d.Fac_Code equals dr.Fac_Code
                  select(d.First_Article)).Sum();

        return result;
    }
}