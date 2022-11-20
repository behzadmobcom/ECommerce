using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.API.Interface;

public interface IMessageRepository : IAsyncRepository<Message>
{
    Task<PagedList<Message>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
}