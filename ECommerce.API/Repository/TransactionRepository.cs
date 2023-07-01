using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Entities.ViewModel;

namespace ECommerce.API.Repository;

public class TransactionRepository : AsyncRepository<Transaction>, ITransactionRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public TransactionRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<Transaction>> Search(TransactionFiltreViewModel transactionFiltreViewModel ,
        CancellationToken cancellationToken)
    {
        var query = _context.Transactions.Where(x => x.User.UserName.Contains(transactionFiltreViewModel.PaginationParameters.Search))
            .Include(d => d.PaymentMethod)
            .Include(x => x.User)
            .AsNoTracking();

        if (transactionFiltreViewModel.UserId > 0) query = query.Where(x => x.UserId == transactionFiltreViewModel.UserId);
        if (transactionFiltreViewModel.ToTransactionDate!= null) query = query.Where(x => x.TransactionDate <= transactionFiltreViewModel.ToTransactionDate);
        if (transactionFiltreViewModel.FromTransactionDate!= null) query = query.Where(x => x.TransactionDate >= transactionFiltreViewModel.FromTransactionDate);
        if (transactionFiltreViewModel.MinimumAmount != null) query = query.Where(x => x.Amount >= transactionFiltreViewModel.MinimumAmount);
        if (transactionFiltreViewModel.MaximumAmount != null) query = query.Where(x => x.Amount <= transactionFiltreViewModel.MaximumAmount);
        if (transactionFiltreViewModel.PaymentMethodStatus != null) query = query.Where(x => x.PaymentMethod.PaymentMethodStatus == transactionFiltreViewModel.PaymentMethodStatus);


        var sortedQuery = query.OrderByDescending(x => x.Id);
        switch (transactionFiltreViewModel.TransactionSort)
        {
            case TransactionSort.LowToHighPiceBuying:
                sortedQuery = query.OrderBy(x => x.Amount);
                break;
            case TransactionSort.HighToLowPriceBuying:
                sortedQuery = query.OrderByDescending(x => x.Amount);
                break;
            case TransactionSort.LowToHighDateBuying:
                sortedQuery = query.OrderBy(x => x.TransactionDate);
                break;
            case TransactionSort.HighToLowDateBuying:
                sortedQuery = query.OrderByDescending(x => x.TransactionDate);
                break;

        }

        var transactionList = await sortedQuery.Select(p => new Transaction
        {
            Id = p.Id,
            Amount = p.Amount,
            TransactionDate = p.TransactionDate,
            PaymentMethod = p.PaymentMethod,
            UserId = p.UserId,
        }).ToListAsync(cancellationToken);

        return PagedList<Transaction>.ToPagedList(transactionList,
            transactionFiltreViewModel.PaginationParameters.PageNumber,
            transactionFiltreViewModel.PaginationParameters.PageSize);
    }
}