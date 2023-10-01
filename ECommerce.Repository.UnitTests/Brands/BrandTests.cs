using Ecommerce.Entities;
using ECommerce.API.DataContext;
using ECommerce.API.Repository;
using Xunit;

namespace ECommerce.Repository.UnitTests.Brands
{
    public class BrandTests
    {
        private readonly SunflowerECommerceDbContext _dbContext;

        public BrandTests()
        {
            var db = new DbContextFake();
            _dbContext = db.GetDatabaseContext();
        }

        [Fact]
        public async Task AddAsync_AddNewEntity_ReturnsNewEntity()
        {
            //Arrange
            int id = 1;
            string name = "Brand for test";
            var brandRepository = new BrandRepository(_dbContext);
            Brand brand = new Brand
            {
                Id = id,
                Name = name
            };

            //Act
            var newBrand = await brandRepository.AddAsync(brand,new CancellationToken());

            //Assert
            Assert.Equal(newBrand.Id,id);
            Assert.Equal(newBrand.Name,name);
        }
    }
}
