using Entities;
using Entities.Helper;
using Entities.ViewModel;

namespace ECommerce.Services.IServices;

public interface IBlogCategoryService : IEntityService<BlogCategory>
{
    Task<ServiceResult<List<BlogCategory>>> GetAll(string search = "", int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult<Dictionary<int, string>>> LoadDictionary();
    Task<ServiceResult> Add(BlogCategory blogCategory);
    Task<ServiceResult> Edit(BlogCategory blogCategory);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<BlogCategory>> GetById(int id);
    Task<ServiceResult<List<CategoryParentViewModel>>> GetParents(int blogId = 0);
}