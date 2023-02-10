using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.Extensions.Caching.Memory;

namespace ECommerce.Services.Services;

    public class ProductCommentService: EntityService<ProductComment> , IProductCommentService
{
        private const string Url = "api/ProductComments";
        private readonly IHttpService _http;
        private readonly IMemoryCache _cache;
    public ProductCommentService(IHttpService http,IMemoryCache cache):base(http)
        {
            _http = http;
            _cache= cache;
        }

        public async Task<ServiceResult> Add(ProductComment productComment)
        {
            var result = await Create(Url, productComment);
            return Return(result);
        }

    public async Task<ServiceResult> Delete(int id)
    {
        var result = await Delete(Url, id);
        //_productAttributes = null;
        return Return(result);
    }

    public async Task<ServiceResult> Edit(ProductComment productComment)
    {
        var result = await Update(Url, productComment);
        //_productAttributes = null;
        return Return(result);
    }

    public async Task<ServiceResult<List<ProductComment>>> GetAllAccesptedComments(string search = "", int pageNumber = 0, int pageSize = 10)
    {
        var result = await ReadList(Url, $"GetAllAccesptedComments?PageNumber={pageNumber}&PageSize={pageSize}&Search={search}");
        return Return(result);
    }

    public async Task<ServiceResult<ProductComment>> GetById(int id)
    {
        var cachEntry = await _cache.GetOrCreate("GetById", async entry =>
        {
            var result = await _http.GetAsync<ProductComment>(Url, $"GetById?id={id}");
            return result;
        });
       
        return Return(cachEntry);
    }

    public async Task<ServiceResult<List<ProductComment>>> Load(string search = "", int pageNumber = 0, int pageSize = 10)
    {
        var result = await ReadList(Url, $"Get?PageNumber={pageNumber}&PageSize={pageSize}&Search={search}");
        return Return(result);
    }
   
}

