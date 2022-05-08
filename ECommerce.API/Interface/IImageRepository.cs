using Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Utilities;
using Entities.Helper;

namespace API.Interface
{
    public interface IImageRepository : IAsyncRepository<Image>
    {
        Task<PagedList<Image>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
        Task<int> DeleteByName(string name, CancellationToken cancellationToken);
        Task<List<Image>> GetByProductId(int productId, CancellationToken cancellationToken);
    }
}
