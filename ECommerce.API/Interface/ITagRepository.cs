using API.Utilities;
using Entities;
using Entities.Helper;
using Entities.ViewModel;

namespace API.Interface;

public interface ITagRepository : IAsyncRepository<Tag>
{
    Task<PagedList<Tag>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    Task<Tag> GetByTagText(string tagText, CancellationToken cancellationToken);
    Task<List<TagProductId>> GetByProductId(int productId, CancellationToken cancellationToken);
}