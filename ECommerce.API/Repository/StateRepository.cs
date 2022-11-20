using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repository;

public class StateRepository : AsyncRepository<State>, IStateRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public StateRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<State> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _context.States.Where(x => x.Name == name).FirstOrDefaultAsync(cancellationToken);
    }
}