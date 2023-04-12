using ECommerce.API.DataContext;
using Ecommerce.Entities.HolooEntity;
using ECommerce.API.Interface;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repository
{
    public class HolooSarfaslRepository : HolooRepository<HolooSarfasl>, IHolooSarfaslRepository
    {
        private readonly HolooDbContext _context;

        public HolooSarfaslRepository(HolooDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<string> Add(string username, CancellationToken cancellationToken)
        {
            var lastSarfaslCode = await _context.Sarfasl.Where(c => c.Col_Code == "103").OrderByDescending(x => x.Moien_Code).FirstOrDefaultAsync();
            var lastMoeinCode = lastSarfaslCode == null ? "0000" : lastSarfaslCode.Moien_Code;
            var newMoeinCode = (Convert.ToInt32(lastMoeinCode) + 1).ToString("D4");
            var sarfasl = new HolooSarfasl
            {
                Col_Code = "103",
                Moien_Code = newMoeinCode,
                Tafzili_Code = "",
                Sarfasl_Code = $"103{newMoeinCode}",
                Sarfasl_Name = username.Trim(),
                Mandeh = 0,
                Group = 1,
                Mahiat = 1,
                Can_Delete = false,
                AutoUse = false,
                Parent = 6,
                Type = 5,
                SParent = 0,
                ArzId = 1,
                Money_Price = 1,
                Selected = false
            };

            await _context.Sarfasl.AddAsync(sarfasl, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return newMoeinCode;
        }
    }
}
