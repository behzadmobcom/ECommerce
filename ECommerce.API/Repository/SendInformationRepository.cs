using API.DataContext;
using API.Interface;
using Entities;

namespace API.Repository
{
    public class SendInformationRepository : AsyncRepository<SendInformation>, ISendInformationRepository
    {
        private readonly SunflowerECommerceDbContext _context;
        public SendInformationRepository(SunflowerECommerceDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
