using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.API.Interface;

public interface IContactRepository : IAsyncRepository<Contact>
{
    Task<PagedList<Contact>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    Task<Contact?> GetByName(string name, CancellationToken cancellationToken);
    Task<Contact?> GetByEmail(string email, CancellationToken cancellationToken);
    Task<Contact?> GetRepetitive(Contact contact, CancellationToken cancellationToken);
}