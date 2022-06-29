using Entities;
using Entities.Helper;

namespace ECommerce.Services.IServices;

public interface IProductAttributeGroupService : IEntityService<ProductAttributeGroup>
{
    Task<ServiceResult<List<ProductAttributeGroup>>> GetAll();
    Task<ServiceResult<List<ProductAttributeGroup>>> Load(string search = "", int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult> Add(ProductAttributeGroup productAttributeGroup);
    Task<ServiceResult> Edit(ProductAttributeGroup productAttributeGroup);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<ProductAttributeGroup>> GetById(int id);
    Task<ServiceResult<List<ProductAttributeGroup>>> GetByProductId(int productId);
    Task<ServiceResult> AddWithAttributeValue(List<ProductAttributeGroup> attributeGroups, int productId);
}