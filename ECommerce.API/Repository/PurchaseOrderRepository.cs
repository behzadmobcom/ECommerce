using API.DataContext;
using API.Interface;
using API.Utilities;
using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class PurchaseOrderRepository : AsyncRepository<PurchaseOrder>, IPurchaseOrderRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public PurchaseOrderRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<PurchaseOrder>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return PagedList<PurchaseOrder>.ToPagedList(
            await _context.PurchaseOrders.Where(x => x.User.UserName.Contains(paginationParameters.Search))
                .AsNoTracking().Include(i => i.PurchaseOrderDetails).OrderBy(on => on.Id)
                .ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }

    public async Task<PurchaseOrder?> GetByUser(int id, CancellationToken cancellationToken) => await _context.PurchaseOrders.Where(x => x.UserId == id).Include(x => x.PurchaseOrderDetails).Include(a => a.SendInformation)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<IEnumerable<PurchaseOrderViewModel>> GetProductListByUserId(int userId,
        CancellationToken cancellationToken)
    {
        var purchaseOrderViewModel = await _context.PurchaseOrderDetails
            .Where(x => x.PurchaseOrder!.UserId == userId && !x.PurchaseOrder.IsPaid &&
                        x.PurchaseOrder.Status == Status.New)
            .Select(p => new PurchaseOrderViewModel
            {
                Id = p.Id,
                ProductId = p.ProductId,
                Url = p.Product.Url,
                Name = p.Product.Name,
                Price = p.Product.Prices!.First(x => x.Id == p.PriceId),
                PriceAmount = p.Product.Prices!.First(x=>x.Id== p.PriceId).Amount,
                PriceId = p.PriceId,
                ImagePath = $"{p.Product.Images!.FirstOrDefault()!.Path}/{p.Product.Images!.FirstOrDefault()!.Name}",
                Brand = p.Product.Brand!.Name,
                Alt = p.Product.Images!.FirstOrDefault()!.Alt,
                //Exist = p.Product.Exist,
                IsColleague = p.PurchaseOrder!.User!.IsColleague,
                UserId = p.PurchaseOrder.UserId,
                Quantity = p.Quantity,
                SumPrice = p.Quantity* p.Product.Prices!.First(x => x.Id == p.PriceId).Amount
            })
            .ToListAsync(cancellationToken);
        return purchaseOrderViewModel;
    }
}