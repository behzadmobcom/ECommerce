using ECommerce.API.DataContext;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Repository.UnitTests.Base
{
    public class DbContextFake : DbContext
    {
        private readonly SunflowerECommerceDbContext _databaseContext;
        private readonly HolooDbContext _holooDatabaseContext;

        public DbContextFake()
        {
            var options = new DbContextOptionsBuilder<SunflowerECommerceDbContext>()
                .UseInMemoryDatabase(databaseName: "SunFlower")
                .Options;
            var optionsHoloo = new DbContextOptionsBuilder<HolooDbContext>()
                .UseInMemoryDatabase(databaseName: "Holoo")
                .Options;
            _databaseContext = new SunflowerECommerceDbContext(
                options,
                new EphemeralDataProtectionProvider(),
                new ConfigurationManager());
            _holooDatabaseContext = new HolooDbContext(
                optionsHoloo);

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

        public HolooDbContext CreateHolooDatabaseContext()
        {
            _holooDatabaseContext.Database.EnsureCreated();
            return _holooDatabaseContext;
        }

        public void DeleteHolooDatabaseContact()
        {
            _holooDatabaseContext.Database.EnsureDeleted();
        }
    }
}
