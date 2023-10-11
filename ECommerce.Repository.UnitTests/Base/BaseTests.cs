using Ecommerce.Entities;
using ECommerce.API.DataContext;

namespace ECommerce.Repository.UnitTests.Base
{
    public abstract class BaseTests : IDisposable
    {

        public readonly CancellationToken CancellationToken;
        public readonly DbContextFake Db;
        public readonly SunflowerECommerceDbContext DbContext;
        public readonly HolooDbContext HolooDbContext;

        protected BaseTests()
        {
            Db = new();
            CancellationToken = new CancellationToken();
            DbContext = Db.CreateDatabaseContext();
            HolooDbContext = Db.CreateHolooDatabaseContext();
        }

        public async Task<Product> GetProduct()
        {
            //Arrange
            Brand brand = new Brand { Id = 1, Name = "brandName" };
            Image image = new Image { Id = 1, Path = "imagePath", Name = "ImageName", Alt = "ImageAlt" };
            Product product = new Product { Id = 1, Name = "productName", Url = "productUrl", BrandId = 1, Brand = brand, Images = new List<Image> { image } };
            Price price = new Price { Id = 1, ProductId = product.Id };
            product!.Prices!.Add(price!);
            price.Product = product;

            await DbContext.Brands.AddAsync(brand, CancellationToken);
            await DbContext.Images.AddAsync(image, CancellationToken);
            await DbContext.Products.AddAsync(product, CancellationToken);
            await DbContext.Prices.AddAsync(price, CancellationToken);
          
            return product;
        }

        public void Dispose()
        {
            Db.DeleteDatabaseContact();
            Db.DeleteHolooDatabaseContact();
        }
    }
}
