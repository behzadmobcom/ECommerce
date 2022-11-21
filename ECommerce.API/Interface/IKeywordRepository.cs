using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.API.Interface;

public interface IKeywordRepository : IAsyncRepository<Keyword>
{
    Task<PagedList<Keyword>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    Task<Keyword> GetByKeywordText(string keywordText, CancellationToken cancellationToken);
    Task<int> AddAll(IEnumerable<Keyword> keywords, CancellationToken cancellationToken);
    Task<List<Keyword>> GetByProductId(int productId, CancellationToken cancellationToken);
}