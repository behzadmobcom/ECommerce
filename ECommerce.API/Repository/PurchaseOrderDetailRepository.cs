using API.DataContext;
using API.Interface;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class PurchaseOrderDetailRepository : AsyncRepository<PurchaseOrderDetail>, IPurchaseOrderDetailRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public PurchaseOrderDetailRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<PurchaseOrderDetail>> GetByPurchaseOrderId(int id, CancellationToken cancellationToken)
    {
        return await _context.PurchaseOrderDetails.Where(x => x.PurchaseOrderId == id).Include(x => x.Price)
            .ToListAsync(cancellationToken);
    }
}