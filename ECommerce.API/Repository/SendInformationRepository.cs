using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using Ecommerce.Entities;

namespace ECommerce.API.Repository;

public class SendInformationRepository : AsyncRepository<SendInformation>, ISendInformationRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public SendInformationRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }
}