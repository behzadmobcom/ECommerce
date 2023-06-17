using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;

namespace ECommerce.API.Interface;

public interface IBlogRepository : IAsyncRepository<Blog>
{
    Task<PagedList<BlogViewModel>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    Task<PagedList<Blog>> GetByTagText(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    Task<Blog> GetByTitle(string title, CancellationToken cancellationToken);

    Task<Blog> AddWithRelations(BlogViewModel blogViewModel, CancellationToken cancellationToken);

    Task<Blog> EditWithRelations(BlogViewModel blogViewModel, CancellationToken cancellationToken);

    Task<IEnumerable<Blog>> GetWithInclude(int id, CancellationToken cancellationToken);

    Task<Blog> GetByUrl(string url, CancellationToken cancellationToken);
    IQueryable<Blog> GetBlogByIdWithInclude(int blogId);
    IQueryable<Blog> GetBlogByUrlWithInclude(string blogUrl);
}