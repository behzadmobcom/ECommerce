using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;

namespace ECommerce.Services.IServices;

public interface ICategoryService : IEntityService<Category>
{
    Task<ServiceResult<CategoryViewModel>> GetByUrl(string url);
    Task<ServiceResult<List<Category>>> Load(string search = "", int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult<List<TreeViewModel>>> LoadTree();
    Task<ServiceResult> Add(Category category);
    Task<ServiceResult> Edit(Category category);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<Category>> GetById(int id);
    Task<ServiceResult<List<CategoryViewModel>>> GetCategoriesByProductId(int productId);
    Task<ServiceResult<List<CategoryParentViewModel>>> GetParents(int productId = 0);
}