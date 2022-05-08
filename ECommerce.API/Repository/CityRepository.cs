using API.DataContext;
using API.Interface;
using Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class CityRepository : AsyncRepository<City>, ICityRepository
    {
        private readonly SunflowerECommerceDbContext _context;
        public CityRepository(SunflowerECommerceDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<City> GetByName(string name, CancellationToken cancellationToken) => await _context.Cities.Where(x => x.Name == name).FirstOrDefaultAsync(cancellationToken);
    }
}
