using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repository;

public class CityRepository : AsyncRepository<City>, ICityRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public CityRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<City> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _context.Cities.Where(x => x.Name == name).FirstOrDefaultAsync(cancellationToken);
    }
}