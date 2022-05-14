using Entities;
using Entities.Helper;
using Entities.ViewModel;

namespace Services.IServices;

public interface ITagService : IEntityService<Tag>
{
    Task<ServiceResult<List<Tag>>> GetAll();
    Task<ServiceResult<List<Tag>>> Filtering(string filter);
    Task<ServiceResult<List<Tag>>> Load(string search = "", int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult<Dictionary<int, string>>> LoadDictionary();
    Task<ServiceResult> Add(Tag tag);
    Task<ServiceResult> Edit(Tag tag);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<Tag>> GetById(int id);
    Task<ServiceResult<List<TagProductId>>> GetTagsByProductId(int productId);
}