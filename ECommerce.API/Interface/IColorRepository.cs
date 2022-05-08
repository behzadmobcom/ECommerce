using System.Threading;
using System.Threading.Tasks;
using API.Utilities;
using Entities;
using Entities.Helper;

namespace API.Interface
{
    public interface IColorRepository : IAsyncRepository<Color>
    {
        Task<PagedList<Color>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
        Task<Color> GetByName(string name, CancellationToken cancellationToken);
    }
}
