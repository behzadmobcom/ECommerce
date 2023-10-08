using ECommerce.API.DataContext;
using ECommerce.Repository.UnitTests.Brands;

namespace ECommerce.Repository.UnitTests.Base
{
    public abstract class BaseTests : IDisposable
    {

        public readonly CancellationToken CancellationToken;
        public readonly DbContextFake Db;
        public readonly SunflowerECommerceDbContext DbContext;

        protected BaseTests()
        {
            Db = new();
            CancellationToken = new CancellationToken();
            DbContext = Db.CreateDatabaseContext();
        }

        public void Dispose()
        {
            Db.DeleteDatabaseContact();
        }
    }
}
