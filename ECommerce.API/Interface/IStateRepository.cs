using Ecommerce.Entities;
using ECommerce.API.Utilities;
using Ecommerce.Entities.Helper;

namespace ECommerce.API.Interface;

public interface IStateRepository : IAsyncRepository<State>
{
    Task<State> GetByName(string name, CancellationToken cancellationToken);
    Task<PagedList<State>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
}