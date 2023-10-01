using ECommerce.API.DataContext;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Repository.UnitTests
{
    public class DbContextFake
    {
        public SunflowerECommerceDbContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<SunflowerECommerceDbContext>()
                .UseInMemoryDatabase(databaseName: "SunflowerECommerceDbContext")
                .Options;
            var databaseContext = new SunflowerECommerceDbContext(options,new EphemeralDataProtectionProvider(),new ConfigurationManager());
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }
    }
}
