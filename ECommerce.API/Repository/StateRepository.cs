using API.DataContext;
using API.Interface;
using Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class StateRepository : AsyncRepository<State>, IStateRepository
    {
        private readonly SunflowerECommerceDbContext _context;
        public StateRepository(SunflowerECommerceDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<State> GetByName(string name, CancellationToken cancellationToken) => await _context.States.Where(x => x.Name == name).FirstOrDefaultAsync(cancellationToken);
    }
}
