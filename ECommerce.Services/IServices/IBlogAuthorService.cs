using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using Entities.Helper;

namespace Services.IServices
{
    public interface IBlogAuthorService : IEntityService<BlogAuthor>
    {
        Task<ServiceResult<List<BlogAuthor>>> Filtering(string filter);
        Task<ServiceResult<List<BlogAuthor>>> Load(string search = "", int pageNumber = 0, int pageSize = 10);
        Task<ServiceResult<Dictionary<int, string>>> LoadDictionary();
        Task<ServiceResult> Add(BlogAuthor blogAuthor);
        Task<ServiceResult> Edit(BlogAuthor blogAuthor);
        Task<ServiceResult> Delete(int id);
        Task<ServiceResult<BlogAuthor>> GetById(int id);
    }
}
