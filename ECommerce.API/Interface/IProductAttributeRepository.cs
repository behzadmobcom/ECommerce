using Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Utilities;
using Entities.Helper;

namespace API.Interface
{
    public interface IProductAttributeRepository : IAsyncRepository<ProductAttribute>
    {
        Task<PagedList<ProductAttribute>> Search(PaginationParameters paginationParameters,
            CancellationToken cancellationToken);
        Task<ProductAttribute> GetByTitle(string title, CancellationToken cancellationToken);
        Task<IEnumerable<ProductAttribute>> GetAll(int productId, CancellationToken cancellationToken);
        Task<IEnumerable<ProductAttribute>> GetAllAttributeWithGroupId(int groupId, int productId, CancellationToken cancellationToken);
    }
}
