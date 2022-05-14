using Entities.Helper;
using Entities.ViewModel;

namespace Services.IServices;

public interface IProductService : IEntityService<ProductViewModel>
{
    Task<ServiceResult<ProductViewModel>> GetProduct(string productUrl);
    ServiceResult CheckBeforeSend(ProductViewModel product);
    Task<ServiceResult<ProductViewModel>> Add(ProductViewModel productViewModel);
    Task<ServiceResult> Edit(ProductViewModel productViewModel);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<ProductViewModel>> FillProductEdit(ProductViewModel productViewModel);
    Task<ServiceResult<PaginationViewModel>> Search(string searchText, int page, int quantityPerPage = 9);

    Task<ServiceResult<List<ProductIndexPageViewModel>>> TopProducts(string search = "", int pageNumber = 0,
        int pageSize = 10, int productSort = 1);

    Task<ServiceResult<List<ProductIndexPageViewModel>>> TopDiscountProducts(int count = 10);
    Task<ServiceResult<List<ProductIndexPageViewModel>>> TopRelativesProduct(int productId, int count = 3);
    Task<ServiceResult<List<ProductIndexPageViewModel>>> TopSellsProducts(int count = 8);
    Task<ServiceResult<List<ProductIndexPageViewModel>>> ProductsWithIdsForCart(List<int> productIdList);
    Task<ServiceResult<List<ProductCompareViewModel>>> ProductsWithIdsForCompare(List<int> productIdList);
    Task<ServiceResult<ProductViewModel>> GetById(int id);

    Task<ServiceResult<List<ProductIndexPageViewModel>>> GetProductList(int categoryId, List<int> brandsId,
        int starCount, int tagId, int pageNumber = 0, int pageSize = 12, int productSort = 1);
}