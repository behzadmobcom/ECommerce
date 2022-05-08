using API.DataContext;
using API.Interface;
using Entities;
using System.Linq;

namespace API.Repository
{
    public class SettingRepository : AsyncRepository<Setting>, ISettingRepository
    {
        private readonly SunflowerECommerceDbContext _context;
        public SettingRepository(SunflowerECommerceDbContext context) : base(context)
        {
            _context = context;
        }

        public string IsDollar() => _context.Settings.First(x => x.Name.Equals("Currency")).Value;
    }
}
