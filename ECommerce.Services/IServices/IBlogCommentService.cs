using Entities;
using Entities.Helper;

namespace ECommerce.Services.IServices;

public interface IBlogCommentService : IEntityService<BlogComment>
{
    Task<ServiceResult<List<BlogComment>>> Filtering(string filter);
    Task<ServiceResult<List<BlogComment>>> Load(string search = "", int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult<Dictionary<int, string>>> LoadDictionary();
    Task<ServiceResult> Add(BlogComment blogComment);
    Task<ServiceResult> Edit(BlogComment blogComment);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<BlogComment>> GetById(int id);
}