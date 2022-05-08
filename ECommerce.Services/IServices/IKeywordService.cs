using Entities;

using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Helper;
using Entities.ViewModel;

namespace Services.IServices
{
    public interface IKeywordService : IEntityService<Keyword>
    {
        Task<ServiceResult<List<Keyword>>> GetKeywordsByProductId(int productId);
        Task<ServiceResult<List<Keyword>>> GetAll();
        Task<ServiceResult<List<Keyword>>> Filtering(string filter);
        Task<ServiceResult<List<Keyword>>> Load(string search = "", int pageNumber = 0, int pageSize = 10);
        Task<ServiceResult<Dictionary<int, string>>> LoadDictionary();
        Task<ServiceResult> Add(Keyword keyword);
        Task<ServiceResult> Edit(Keyword keyword);
        Task<ServiceResult> Delete(int id);
        Task<ServiceResult<Keyword>> GetById(int id);
    }
}
