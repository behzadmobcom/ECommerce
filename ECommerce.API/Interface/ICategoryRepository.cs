using Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Utilities;
using Entities.Helper;
using Entities.ViewModel;

namespace API.Interface
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
        Task<PagedList<Category>> Search(PaginationParameters paginationParameters,
            CancellationToken cancellationToken);
        Task<Category> GetByName(string name, CancellationToken cancellationToken, int? parentId=null);

        Task<int> AddAll(IEnumerable<Category> categories, CancellationToken cancellationToken);

        Task<IEnumerable<int>> GetIdsByUrl(string categoryUrl, CancellationToken cancellationToken);

        Task<CategoryViewModel?> GetByUrl(string categoryUrl, CancellationToken cancellationToken);

        Task<List<CategoryParentViewModel>> Parents(int productId,CancellationToken cancellationToken);

        Task<List<int>> ChildrenCategory(int categoryId, CancellationToken cancellationToken);
    }
}
