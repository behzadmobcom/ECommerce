using API.DataContext;
using API.Interface;
using Entities;

namespace API.Repository;

public class EmployeeRepository : AsyncRepository<Employee>, IEmployeeRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public EmployeeRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }
}