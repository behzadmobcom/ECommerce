using Ecommerce.Entities;
using ECommerce.API.Interface;
using ECommerce.API.Repository;
using ECommerce.Repository.UnitTests.Base;
using Xunit;

namespace ECommerce.Repository.UnitTests.Brands
{
    public class BrandTests : BaseTests
    {
        private readonly IBrandRepository _brandRepository;

        public BrandTests()
        {
            _brandRepository = new BrandRepository(DbContext);
        }

        [Fact]
        public async Task AddAsync_AddNewEntity_ReturnsNewEntity()
        {
            //Arrange
            int id = 1;
            string name = "Brand for test";
            Brand brand = new Brand
            {
                Id = id,
                Name = name
            };

            //Act
            var newBrand = await _brandRepository.AddAsync(brand,new CancellationToken());

            //Assert
            Assert.Equal(newBrand.Id,id);
            Assert.Equal(newBrand.Name,name);
        }
    }
}
