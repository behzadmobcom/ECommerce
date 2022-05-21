using API.DataContext;
using API.Interface;
using Entities.HolooEntity;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class HolooFBailRepository : HolooRepository<HolooFBail>, IHolooFBailRepository
{
    private readonly HolooDbContext _context;
    private readonly ILogger<HolooFBailRepository> _logger;

    public HolooFBailRepository(HolooDbContext context, ILogger<HolooFBailRepository> logger) : base(context)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<string> Add(HolooFBail bail, CancellationToken cancellationToken)
    {
        var lastRow = await _context.FBAILPRE.LastOrDefaultAsync(x=>x.Fac_Type.Equals("P"),cancellationToken);
        var lastFacCode = Convert.ToInt32(lastRow.Fac_Code) + 1;
        bail.Fac_Code_C = lastFacCode;
        bail.Fac_Code = lastFacCode.ToString("000000");
        try
        {
            _context.Add(bail);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result.ToString();
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            lastFacCode += 2;
            bail.Fac_Code_C = lastFacCode;
            bail.Fac_Code = lastFacCode.ToString("000000");
            _context.Add(bail);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result.ToString();
        }
    }

    public async Task<(string fCode,int fCodeC)> GetFactorCode(CancellationToken cancellationToken)
    {
        var holooFBail =await _context.FBAILPRE.LastOrDefaultAsync(x=>x.Fac_Type.Equals("P"),cancellationToken);
        var fCode = "0";
        var fCodeC = 0;
        if (holooFBail != null)
        {
            fCode = (Convert.ToInt32(holooFBail.Fac_Code) + 1).ToString("000000");
            fCodeC = Convert.ToInt32(holooFBail.Fac_Code_C) + 1;
        }
        return (fCode, fCodeC);
    }
}