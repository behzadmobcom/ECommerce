using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;

namespace ECommerce.Services.Services;

    public class ProductCommentService: EntityService<ProductComment> , IProductCommentService
{
        private const string Url = "api/ProductComments";
        private readonly IHttpService _http;
        public ProductCommentService(IHttpService http):base(http)
        {
            _http = http;
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

    public async Task<ServiceResult<List<ProductComment>>> GetAllAccesptedComments(int productId)
    {
        var result = await ReadList(Url, $"GetAllAccesptedComments?productId={productId}");       
        return Return(result);
    }

    public async Task<ServiceResult<ProductComment>> GetById(int id)
    {
        var result = await _http.GetAsync<ProductComment>(Url, $"GetById?id={id}");
        return Return(result);
    }

    public async Task<ServiceResult<List<ProductComment>>> Load(string search = "", int pageNumber = 0, int pageSize = 10)
    {
        var result = await ReadList(Url, $"Get?PageNumber={pageNumber}&PageSize={pageSize}&Search={search}");
        return Return(result);
    }
   
}

