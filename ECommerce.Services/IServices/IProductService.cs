using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Services.IServices;

public interface IProductService : IEntityService<ProductViewModel>
{
    Task<ServiceResult<ProductViewModel>> GetProduct(string productUrl);
    ServiceResult CheckBeforeSend(ProductViewModel product);
    Task<ServiceResult<ProductViewModel>> Add(ProductViewModel productViewModel);
    Task<ServiceResult> Edit(ProductViewModel productViewModel);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<ProductViewModel>> FillProductEdit(ProductViewModel productViewModel);
    Task<ServiceResult<List<ProductIndexPageViewModel>>> Search(string searchText, int page, int quantityPerPage = 9);

    Task<ServiceResult<List<ProductIndexPageViewModel>>> TopProducts(string CategoryId = "", string search = "",
      int pageNumber = 0, int pageSize = 10, int productSort = 1, int? endPrice = null, int? startPrice = null,
      bool isExist = false, bool isWithoutBail = false, string tagText = "");


    Task<ServiceResult<List<ProductIndexPageViewModel>>> TopNew(int count = 10, bool isWithoutBail = false);
    Task<ServiceResult<List<ProductIndexPageViewModel>>> TopNewShop(int count = 3,bool isWithoutBail = false);
    Task<ServiceResult<List<ProductIndexPageViewModel>>> TopPrice(int count = 10, bool isWithoutBail = false);
    Task<ServiceResult<List<ProductIndexPageViewModel>>> TopDiscount(int count = 10, bool isWithoutBail = false);
    Task<ServiceResult<List<ProductIndexPageViewModel>>> TopRelatives(int productId, int count = 3, bool isWithoutBail = false);
    Task<ServiceResult<List<ProductIndexPageViewModel>>> TopSells(int count = 8, bool isWithoutBail = false);
    Task<ServiceResult<List<ProductIndexPageViewModel>>> TopStars(int count = 8, bool isWithoutBail = false);
    Task<ServiceResult<List<ProductIndexPageViewModel>>> ProductsWithIdsForCart(List<int> productIdList);
    Task<ServiceResult<List<ProductCompareViewModel>>> ProductsWithIdsForCompare(List<int> productIdList);
    Task<ServiceResult<ProductViewModel>> GetById(int id);
    Task<ServiceResult<ProductModalViewModel>> GetByIdViewModel(int id);
    Task<ServiceResult<List<ProductIndexPageViewModel>>> GetProductList(int categoryId, List<int> brandsId,
        int starCount, int tagId, int pageNumber = 0, int pageSize = 12, int productSort = 1);
    Task<ServiceResult<List<ProductIndexPageViewModel>>> GetTops(string includeProperties, bool isWithoutBail = false);

}