using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;

namespace ECommerce.API.Interface;

public interface ITagRepository : IAsyncRepository<Tag>
{
    Task<PagedList<Tag>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    Task<List<TagProductId>> GetByProductId(int productId, CancellationToken cancellationToken);
    Task<Tag> GetByTagText(string tagText, CancellationToken cancellationToken);
    Task<List<Tag>> GetAllProductTags(CancellationToken cancellationToken);
   
}