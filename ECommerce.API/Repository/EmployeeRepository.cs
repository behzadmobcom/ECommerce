using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repository;

public class EmployeeRepository : AsyncRepository<Employee>, IEmployeeRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public EmployeeRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<Employee>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return PagedList<Employee>.ToPagedList(
            await _context.Employees.Where(x => x.Name.Contains(paginationParameters.Search)).AsNoTracking()
                .OrderBy(on => on.Id).ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }

    public async Task<Employee> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _context.Employees.Where(x => x.Name == name).FirstOrDefaultAsync(cancellationToken);
    }




}