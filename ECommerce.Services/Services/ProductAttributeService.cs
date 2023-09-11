using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;

namespace ECommerce.Services.Services;

public class ProductAttributeService : EntityService<ProductAttribute>, IProductAttributeService
{
    private const string Url = "api/ProductAttributes";

    //private List<ProductAttribute> _productAttributes;
    private readonly IHttpService _http;

    //public int ProductAttributeGroupsId { get; set; }

    public ProductAttributeService(IHttpService http) : base(http)
    {
        _http = http;
    }

    public async Task<ServiceResult<List<ProductAttribute>>> Load(int productId = 0, int pageNumber = 0,
        int pageSize = 10)
    {
        var result = productId == 0 ? await ReadList(Url) : await ReadList(Url, $"GetAll/{productId}");
        return Return(result);
    }

    public async Task<ServiceResult> Add(ProductAttribute productAttribute)
    {
        var result = await Create(Url, productAttribute);
        //_productAttributes = null;
        return Return(result);
    }

    public async Task<ServiceResult> Edit(ProductAttribute productAttribute)
    {
        var result = await Update(Url, productAttribute);
        //_productAttributes = null;
        return Return(result);
    }

    public async Task<ServiceResult> Delete(int id)
    {
        //var result = await Delete(Url, id);
        //return Return(result);
        var result = await _http.DeleteAsync(Url, id);
        if (result.Code == ResultCode.Success)
        {
            return new ServiceResult
            {
                Code = ServiceCode.Success,
                Message = "با موفقیت حذف شد"
            };
        }
        return new ServiceResult { Code = ServiceCode.Error, Message = "به علت وابستگی با عناصر دیگر امکان حذف وجود ندارد" };
    }

    public async Task<ServiceResult<List<ProductAttribute>>> GetAllAttributeWithGroupId(int attributeGroupsId,
        int productId)
    {
        var result = await ReadList(Url, $"GetAllAttributeByGroupId?groupId={attributeGroupsId}&productId={productId}");
        return Return(result);
    }

    public async Task<ServiceResult<List<ProductAttribute>>> GetAllAttributeWithProductId(int productId)
    {
        var result = await ReadList(Url, $"GetAllAttributeByProductId?productId={productId}");
        return Return(result);
    }

    public async Task<ServiceResult<List<ProductAttribute>>> GetAllAttributeWithAttributeId(int attributeId)
    {
        var result = await ReadList(Url, $"GetById?id={attributeId}");
        return Return(result);
    }

    public async Task<ServiceResult<List<ProductAttribute>>> LoadWithValue(int productId)
    {
        var result = await ReadList(Url, $"GetAll?productId={productId}");
        return Return(result);
    }

    public async Task<ServiceResult<ProductAttribute>> GetById(int id)
    {
        var result = await _http.GetAsync<ProductAttribute>(Url, $"GetById?id={id}");
        return Return(result);
    }
}