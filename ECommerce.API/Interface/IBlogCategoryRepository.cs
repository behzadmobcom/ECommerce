using System.Threading;
using Entities;
using System.Threading.Tasks;

namespace API.Interface
{
    public interface IBlogCategoryRepository : IAsyncRepository<BlogCategory>
    {
        Task<BlogCategory> GetByName(string name,int? parentId, CancellationToken cancellationToken);
    }
}
