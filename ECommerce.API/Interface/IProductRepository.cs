using Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities.ViewModel;

namespace API.Interface
{
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

        Task<Product?> GetProductByIdWithInclude(int productId, CancellationToken cancellationToken);

        Task<List<Product>> TopNew(int count, CancellationToken cancellationToken);

        Task<List<Product>> TopPrices(int count, CancellationToken cancellationToken);

        Task<List<Product>> TopChip(int count, CancellationToken cancellationToken);

        Task<List<Product>> TopDiscounts(int count, CancellationToken cancellationToken);

        Task<List<Product>> TopStars(int count, CancellationToken cancellationToken);

        Task<List<Product>> TopSells(int count, CancellationToken cancellationToken);

        Task<List<Product>> TopRelatives(int productId, int count, CancellationToken cancellationToken);

        Task<List<Product>> GetByCategoryId(int categoryId, CancellationToken cancellationToken);

        IQueryable<Product> GetProducts(List<int>? brandsId, List<int>? starsCount, List<int>? tagsId);

    }
}
