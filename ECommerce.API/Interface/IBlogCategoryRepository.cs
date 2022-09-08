using API.Utilities;
using Entities;
using Entities.Helper;
using Entities.ViewModel;

namespace API.Interface;

public interface IBlogCategoryRepository : IAsyncRepository<BlogCategory>
{
    Task<PagedList<BlogCategory>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken);

    Task<BlogCategory?> GetByName(string name, int? parentId, CancellationToken cancellationToken);

    Task<List<CategoryParentViewModel>> Parents(int blogId, CancellationToken cancellationToken);
}