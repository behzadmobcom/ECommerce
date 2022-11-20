using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using Ecommerce.Entities;

namespace ECommerce.API.Repository;

public class TransactionRepository : AsyncRepository<Transaction>, ITransactionRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public TransactionRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }
}