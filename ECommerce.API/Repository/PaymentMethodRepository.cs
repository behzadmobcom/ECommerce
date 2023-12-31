﻿using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repository;

public class PaymentMethodRepository : AsyncRepository<PaymentMethod>, IPaymentMethodRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public PaymentMethodRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<PaymentMethod>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        if (!paginationParameters.Search.Equals(""))
            return PagedList<PaymentMethod>.ToPagedList(
                await _context.PaymentMethods
                    .Where(x => x.Transactions.Any(r => r.RefId.Contains(paginationParameters.Search))).AsNoTracking()
                    .OrderBy(on => @on.Id).ToListAsync(cancellationToken),
                paginationParameters.PageNumber,
                paginationParameters.PageSize);

        return PagedList<PaymentMethod>.ToPagedList(
            await _context.PaymentMethods.AsNoTracking().OrderBy(on => on.Id).ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }

    public async Task<PaymentMethod> GetByAccountNumber(string name, CancellationToken cancellationToken)
    {
        return await _context.PaymentMethods.Where(x => x.AccountNumber == name).FirstOrDefaultAsync();
    }

    public async Task<int> AddAll(IEnumerable<PaymentMethod> paymentMethods, CancellationToken cancellationToken)
    {
        await _context.PaymentMethods.AddRangeAsync(paymentMethods, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }
}