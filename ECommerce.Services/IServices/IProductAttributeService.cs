using Entities;
using Entities.Helper;

namespace Services.IServices;

public interface IProductAttributeService : IEntityService<ProductAttribute>
{
    Task<ServiceResult<List<ProductAttribute>>> Load(int productId = 0, int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult> Add(ProductAttribute productAttribute);
    Task<ServiceResult> Edit(ProductAttribute productAttribute);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<ProductAttribute>> GetById(int id);
    Task<ServiceResult<List<ProductAttribute>>> LoadWithValue(int productId);
    Task<ServiceResult<List<ProductAttribute>>> GetAllAttributeWithGroupId(int attributeGroupsId, int productId);
    Task<ServiceResult<List<ProductAttribute>>> GetAllAttributeWithAttributeId(int attributeId);
    Task<ServiceResult<List<ProductAttribute>>> GetAllAttributeWithProductId(int productId);
}