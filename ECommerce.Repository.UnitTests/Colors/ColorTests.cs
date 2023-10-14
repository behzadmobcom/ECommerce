using Ecommerce.Entities;
using ECommerce.API.Interface;
using ECommerce.API.Repository;
using ECommerce.Repository.UnitTests.Base;
using Xunit;

namespace ECommerce.Repository.UnitTests.Colors
{
    public class ProductTests : BaseTests
    {
        private readonly IColorRepository _colorRepository;

        public ProductTests()
        {
            _colorRepository = new ColorRepository(DbContext);
        }

        [Fact]
        public async Task AddAsync_AddNewEntity_ReturnsSameEntity()
        {
            //Arrange
            int id = 2;
            string name = Guid.NewGuid().ToString();
            string colorCode = Guid.NewGuid().ToString();
            Color color = new Color
            {
                Id = id,
                Name = name,
                ColorCode = colorCode
            };

            //Act
            Color newColor = await _colorRepository.AddAsync(color, CancellationToken);

            //Assert
            Assert.Equal(id, newColor.Id);
            Assert.Equal(name, newColor.Name);
            Assert.Equal(colorCode, newColor.ColorCode);
        }

        [Fact]
        public async Task GetAll_CountAllEntities_ReturnsTwoEntities()
        {
            //Arrange
            int id = 3;
            string name = Guid.NewGuid().ToString();
            string colorCode = Guid.NewGuid().ToString();
            Color color = new Color
            {
                Id = id,
                Name = name,
                ColorCode = colorCode
            };
            await DbContext.Colors.AddAsync(color, CancellationToken);
            await DbContext.SaveChangesAsync(CancellationToken);

            //Act
            var newColor =await _colorRepository.GetAll(CancellationToken);

            //Assert
            Assert.Equal(2, newColor.Count());
        }
    }
}
