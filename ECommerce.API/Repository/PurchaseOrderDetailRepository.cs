using API.DataContext;
using API.Interface;
using Entities;

namespace API.Repository;

public class PurchaseOrderDetailRepository : AsyncRepository<PurchaseOrderDetail>, IPurchaseOrderDetailRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public PurchaseOrderDetailRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }
}