using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Controllers;
using API.DataContext;
using API.Interface;
using Entities.Helper;
using Entities.HolooEntity;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Repository
{
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
            var lastRow = await _context.FBAILPRE.OrderByDescending(x => x.Fac_Code).FirstOrDefaultAsync(cancellationToken);
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
    }
}
