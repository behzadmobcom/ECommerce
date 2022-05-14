using API.DataContext;
using API.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class HolooRepository<T> : IHolooRepository<T> where T : class
{
    protected HolooDbContext Context;

    public HolooRepository(HolooDbContext context)
    {
        Context = context;
    }

    public void Dispose()
    {
        Context.Dispose();
    }

    public async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken)
    {
        return await Context.Set<T>().ToListAsync(cancellationToken);
    }
}