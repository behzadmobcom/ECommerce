using Entities;
using Entities.Helper;
using ECommerce.Services.IServices;

namespace Services.Services;

public class ProductAttributeGroupService : EntityService<ProductAttributeGroup>, IProductAttributeGroupService
{
    private const string Url = "api/ProductAttributeGroups";
    private readonly IHttpService _http;
    private List<ProductAttributeGroup> _productAttributeGroups;


    public ProductAttributeGroupService(IHttpService http) : base(http)
    {
        _http = http;
    }

    public async Task<ServiceResult<List<ProductAttributeGroup>>> GetAll()
    {
        var result = await ReadList(Url, "GetAll");
        return Return(result);
    }

    public async Task<ServiceResult<List<ProductAttributeGroup>>> Load(string search = "", int pageNumber = 0, int pageSize = 10)
    {
        var result = await ReadList(Url, $"Get?PageNumber={pageNumber}&PageSize={pageSize}&Search={search}");
        return Return(result);
    }

    public async Task<ServiceResult<List<ProductAttributeGroup>>> GetByProductId(int productId)
    {
        var result = await ReadList(Url, $"GetByProductId?productId={productId}");
        return Return(result);
    }

    public async Task<ServiceResult<List<ProductAttributeGroup>>> Load(int pageNumber = 0, int pageSize = 10)
    {
        var result = await ReadList(Url, $"Get?PageNumber={pageNumber}&PageSize={pageSize}");
        return Return(result);
    }

    public async Task<ServiceResult> Add(ProductAttributeGroup productAttributeGroup)
    {
        var result = await Create(Url, productAttributeGroup);
        _productAttributeGroups = null;
        return Return(result);
    }

    public async Task<ServiceResult> AddWithAttributeValue(List<ProductAttributeGroup> attributeGroups, int productId)
    {
        var result = await _http.PostAsync(Url, attributeGroups, $"AddWithAttributeValue?ProductId={productId}");
        _productAttributeGroups = null;
        return Return(result);
    }

    public async Task<ServiceResult> Edit(ProductAttributeGroup productAttributeGroup)
    {
        var result = await Update(Url, productAttributeGroup);
        _productAttributeGroups = null;
        return Return(result);
    }

    public async Task<ServiceResult> Delete(int id)
    {
        var result = await Delete(Url, id);
        _productAttributeGroups = null;
        return Return(result);
    }

    public async Task<ServiceResult<ProductAttributeGroup>> GetById(int id)
    {
        var result = await _http.GetAsync<ProductAttributeGroup>(Url, $"GetById?id={id}");
        return Return(result);
    }
}