using ECommerce.API.DataContext;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Repository.UnitTests.Brands
{
    public class DbContextFake : DbContext
    {
        private readonly SunflowerECommerceDbContext _databaseContext;

        public DbContextFake()
        {
            var options = new DbContextOptionsBuilder<SunflowerECommerceDbContext>()
                .UseInMemoryDatabase(databaseName: "SunFlower")
                .Options;
            _databaseContext = new SunflowerECommerceDbContext(
                options,
                new EphemeralDataProtectionProvider(),
                new ConfigurationManager());
        }

        public SunflowerECommerceDbContext CreateDatabaseContext()
        {
            _databaseContext.Database.EnsureCreated();
            return _databaseContext;
        }

        public void DeleteDatabaseContact()
        {
            _databaseContext.Database.EnsureDeleted();
        }
    }
}
