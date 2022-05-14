using API.Utilities;
using Entities;
using Entities.Helper;

namespace API.Interface;

public interface IBlogAuthorRepository : IAsyncRepository<BlogAuthor>
{
    Task<PagedList<BlogAuthor>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken);

    Task<BlogAuthor> GetByName(string name, CancellationToken cancellationToken);
}