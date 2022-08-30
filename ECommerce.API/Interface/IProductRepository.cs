using API.Utilities;
using Entities;
using Entities.Helper;
using Entities.ViewModel;

namespace API.Interface;

public interface IProductRepository : IAsyncRepository<Product>
{
    Task<Product> GetByName(string name, CancellationToken cancellationToken);

    Task<Product> GetByUrl(string url, CancellationToken cancellationToken);

    Task<int> AddAll(IEnumerable<Product?> products, CancellationToken cancellationToken);

    //Task<IEnumerable<Product?>> GetAllHolooProducts(CancellationToken cancellationToken);

    Task<Product?> AddWithRelations(ProductViewModel productViewModel, CancellationToken cancellationToken);

    Task<Product?> EditWithRelations(ProductViewModel productViewModel, CancellationToken cancellationToken);

    IQueryable<Product> GetWithInclude(CancellationToken cancellationToken);

    int GetCategoryProductCount(int categoryId, CancellationToken cancellationToken);

    Task<List<ProductIndexPageViewModel>> GetProductList(List<int> productIdList, CancellationToken cancellationToken);

    Task<List<ProductCompareViewModel>> GetProductListWithAttribute(List<int> productIdList,
        CancellationToken cancellationToken);

    IQueryable<Product> GetProductByIdWithInclude(int productId);

    IQueryable<Product?> GetProducts(List<int> categoriesId, List<int>? brandsId, List<int>? starsCount, List<int>? tagsId);

    Task<List<Product>> GetByCategoryId(int categoryId, CancellationToken cancellationToken);

    Task<PagedList<ProductIndexPageViewModel>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken);

    Task<Product?> GetProductById(int id, CancellationToken cancellationToken);

    #region Tops

    Task<List<ProductIndexPageViewModel>> TopNew(int count, CancellationToken cancellationToken);

    Task<List<ProductIndexPageViewModel>> TopPrices(int count, CancellationToken cancellationToken);

    Task<List<ProductIndexPageViewModel>> TopChip(int count, CancellationToken cancellationToken);

    //Task<List<ProductIndexPageViewModel?>> TopDiscounts(int count, CancellationToken cancellationToken);

    Task<List<ProductIndexPageViewModel>> TopStars(int count, CancellationToken cancellationToken);

    Task<List<ProductIndexPageViewModel>> TopSells(int count, CancellationToken cancellationToken);

    Task<List<ProductIndexPageViewModel>> TopRelatives(int productId, int count, CancellationToken cancellationToken);

    #endregion
}