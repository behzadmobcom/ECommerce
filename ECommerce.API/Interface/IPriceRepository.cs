using Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Utilities;
using Entities.Helper;

namespace API.Interface
{
    public interface IPriceRepository : IAsyncRepository<Price>
    {
        Task<PagedList<Price>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
        Task<int> AddAll(IEnumerable<Price> prices, CancellationToken cancellationToken);

        Task<int> EditAll(IEnumerable<Price> prices, int id, CancellationToken cancellationToken);

        Task<IEnumerable<Price>> PriceOfProduct(int id, CancellationToken cancellationToken);

    }
}
