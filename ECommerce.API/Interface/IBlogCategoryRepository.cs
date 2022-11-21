using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;

namespace ECommerce.API.Interface;

public interface IBlogCategoryRepository : IAsyncRepository<BlogCategory>
{
    Task<PagedList<BlogCategory>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken);

    Task<BlogCategory?> GetByName(string name, int? parentId, CancellationToken cancellationToken);

    Task<List<CategoryParentViewModel>> Parents(int blogId, CancellationToken cancellationToken);
}