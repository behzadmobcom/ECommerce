using API.DataContext;
using API.Interface;
using Entities;

namespace API.Repository;

public class TransactionRepository : AsyncRepository<Transaction>, ITransactionRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public TransactionRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }
}