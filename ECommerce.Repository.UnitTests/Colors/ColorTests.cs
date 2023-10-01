using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.API.Interface;
using ECommerce.API.Repository;
using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using ECommerce.API.DataContext;

namespace ECommerce.Repository.UnitTests.Colors
{
    public class ColorTests
    {
        private readonly IColorRepository _colorRepository;
        private readonly CancellationToken _cancellationToken;

        public ColorTests()
        {
            DbContextFake db = new DbContextFake();
            SunflowerECommerceDbContext dbContext = db.GetDatabaseContext();
            _colorRepository = new ColorRepository(dbContext);
            _cancellationToken = new CancellationToken();
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
            Color newColor = await _colorRepository.AddAsync(color, _cancellationToken);

            //Assert
            Assert.Equal(id, newColor.Id);
            Assert.Equal(name, newColor.Name);
            Assert.Equal(colorCode, newColor.ColorCode);
        }
    }
}
