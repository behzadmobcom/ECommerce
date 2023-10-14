using Ecommerce.Entities;
using ECommerce.API.Interface;
using ECommerce.API.Repository;
using ECommerce.Repository.UnitTests.Base;
using Xunit;

namespace ECommerce.Repository.UnitTests.ProductTests
{
    public class ProductTests : BaseTests
    {
        private readonly IProductRepository _productRepository;

        public ProductTests()
        {
            _productRepository = new ProductRepository(DbContext,);
        }

        [Fact]
        public async Task AddAsync_AddNewEntity_ReturnsSameEntity()
        {
            //Arrange
            int id = 2;
            string name = Guid.NewGuid().ToString();
            string Description = Guid.NewGuid().ToString();
            int MinOrder = 1;
            int MaxOrder = 10;
            int MinInStore = 1;
            int ReorderingLevel = 2;
            bool IsDiscontinued =true;
            bool IsActive = true;
            string Url = "https://github.com";
            int StoreId = 1;
            int SupplierId = 1;
            int BrandId = 1;
            int HolooCompanyId = 1;
            int Star = 1;
            bool IsShowInIndexPage = true;
            string Review = Guid.NewGuid().ToString();

            Product product = new Product
            {
                Id = id,
                Name = name,
                Description = Description,
                MinOrder = MinOrder,
                MaxOrder = MaxOrder,
                MinInStore = MinInStore,
                ReorderingLevel = ReorderingLevel,
                IsDiscontinued = IsDiscontinued,
                IsActive = IsActive,
                Url = Url,
                StoreId = StoreId,
                SupplierId = SupplierId,
                BrandId = BrandId,
                HolooCompanyId = HolooCompanyId,
                Star = Star,
                IsShowInIndexPage = true,   
                Review = Guid.NewGuid().ToString()
                
            };

            //Act
            Product newProduct = await _productRepository.AddAsync(product, CancellationToken);

            //Assert
            Assert.Equal(id, newProduct.Id);
            Assert.Equal(name, newProduct.Name);
            Assert.Equal(Description, newProduct.Description);
            Assert.Equal(MinOrder, newProduct.MinOrder);
            Assert.Equal(MaxOrder, newProduct.MaxOrder);
            Assert.Equal(MinInStore, newProduct.MinInStore);
            Assert.Equal(ReorderingLevel, newProduct.ReorderingLevel);
            Assert.Equal(IsDiscontinued, newProduct.IsDiscontinued);
            Assert.Equal(IsActive, newProduct.IsActive);
            Assert.Equal(Url, newProduct.Url);
            Assert.Equal(StoreId, newProduct.StoreId);
            Assert.Equal(SupplierId, newProduct.SupplierId);
            Assert.Equal(BrandId, newProduct.BrandId);
            Assert.Equal(HolooCompanyId, newProduct.HolooCompanyId);
            Assert.Equal(Star, newProduct.Star);
            Assert.Equal(IsShowInIndexPage, newProduct.IsShowInIndexPage);
            Assert.Equal(Review, newProduct.Review);
        }
    }
}
