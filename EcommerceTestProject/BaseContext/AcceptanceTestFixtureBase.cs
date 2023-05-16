using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using NSubstitute;

namespace ECommerce.ControllersTests.BaseContext
{
    public abstract class AcceptanceTestFixtureBase<TStartup> : IDisposable
        where TStartup : class
    {
        private WebApplicationFactory<TStartup> _webApplicationFactory = new();
        //private AcceptanceTestAuthorizationContextAccessor _acceptanceTestAuthorizationContextAccessor = new();
        protected IServiceProvider ServiceProvider { get; private set; } = GetDefaultServiceProvider();
        public IConfiguration Configuration { get; private set; } = new ConfigurationBuilder().Build();
        public HttpClient HttpClient { get; private set; } = new();
        public string RelativeContentRootPath { get; private set; } = "./";
        public IPlatformMachineAuthenticationService PlatformMachineAuthenticationService { get; private set; }

        protected AcceptanceTestFixtureBase()
        {
            Initialize();
        }

        private void Initialize()
        {
            RelativeContentRootPath = SetRelativeContentRootPath();
            _webApplicationFactory = new WebApplicationFactory<TStartup>()
                .WithWebHostBuilder(ConfigureWebHostBuilder);
            HttpClient = CreateHttpClient();

            // Note: The WebApplicationFactory Server is only created after creating the HttpClient.
            ServiceProvider = _webApplicationFactory.Server.Host.Services;
            Configuration = ServiceProvider.GetRequiredService<IConfiguration>();
        }


        private static IServiceProvider GetDefaultServiceProvider()
        {
            ServiceCollection serviceCollection = new();
            return serviceCollection.BuildServiceProvider();
        }

        private void ConfigureWebHostBuilder(IWebHostBuilder builder)
        {
            builder.UseEnvironment("AcceptanceTests")
                   // Note: The action parameter of ConfigureTestServices runs after Startup.ConfigureServices.
                   .ConfigureTestServices(ConfigureTestServices);

            if (!string.IsNullOrWhiteSpace(RelativeContentRootPath))
            {
                builder.UseSolutionRelativeContentRoot(RelativeContentRootPath);
            }
        }

        public virtual string SetRelativeContentRootPath()
        {
            return string.Empty;
        }

        private void ConfigureTestServices(IServiceCollection services)
        {
            MockAuthorizationAndAuthentication(services);
            RegisterAcceptanceTestAuthorizationContextAccessor(services);

            IConfiguration configuration = services.BuildServiceProvider().GetService<IConfiguration>()!;
            if (!configuration.GetValue<bool>("UseLocalDb"))
            {
                ConfigureInMemoryServices(services);
            }
            ConfigureCommonTestServices(services);
            ConfigureWebHostTestServices(services);
            ConfigureSdkTestServices(services);
        }

        private void MockAuthorizationAndAuthentication(IServiceCollection services)
        {
            //TokenGeneratorService? tokenGeneratorService = new();
            //services.AddSingleton(_ => tokenGeneratorService);
            //services.PostConfigure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        IssuerSigningKey = tokenGeneratorService.SecurityKey,
            //        ValidIssuer = tokenGeneratorService.Issuer,
            //        ValidAudience = tokenGeneratorService.Audience
            //    };
            //});

            //services.RemoveAll<IAuthServerClient>();
            //services.AddSingleton(Substitute.For<IAuthServerClient>());
        }

        private void RegisterAcceptanceTestAuthorizationContextAccessor(IServiceCollection services)
        {
            //_acceptanceTestAuthorizationContextAccessor = new AcceptanceTestAuthorizationContextAccessor();
            //services.AddSingleton<IAuthorizationContextAccessor, AcceptanceTestAuthorizationContextAccessor>(
            //    _ => _acceptanceTestAuthorizationContextAccessor);
        }


        protected virtual void ConfigureInMemoryServices(IServiceCollection services)
        {
        }

        protected virtual void ConfigureCommonTestServices(IServiceCollection services)
        {
        }

        protected virtual void ConfigureWebHostTestServices(IServiceCollection services)
        {
        }
        protected virtual void ConfigureSdkTestServices(IServiceCollection services)
        {
        }

        protected void ConfigureClientSdkForTesting(IServiceCollection services, Action registerOwnClientSdkAction)
        {
            services.AddSingleton((Func<IServiceProvider, IRequesterServiceFactory>)((sp) => CreateRequesterServiceTestFixtureFactory()));
            services.AddWebApiClientSdkDependencies();
            registerOwnClientSdkAction();
        }

        private RequesterServiceTestFixtureFactory CreateRequesterServiceTestFixtureFactory()
        {
            ProvideAuthorizationContext(new AuthorizationContext
            {
                AccessLevel = AccessLevel.Person,
                Actor = new AuthorizationActor
                {
                    Id = "TestActor",
                    Type = ActorType.User,
                    DisplayName = "TestActorDisplayName"
                }
            });
            IAuthorizationContextProvider context = GetService<IAuthorizationContextProvider>();
            RequesterServiceTestFixtureFactory? requesterServiceTestFixtureFactory = new(HttpClient, context);
            return requesterServiceTestFixtureFactory;
        }

        protected void ConfigureMachineClientSdkForTesting(IServiceCollection services, Action registerOwnMachineClientSdkAction)
        {
            services.AddSingleton((Func<IServiceProvider, IRequesterServiceFactory>)((sp) => CreateRequesterServiceMachineClientTestFixtureFactory()));
            //services.AddMachineClientSdkDependencies();
            registerOwnMachineClientSdkAction();
        }

        /// <summary>
        /// This method is used for a machine to machine testing.
        /// </summary>
        /// <returns></returns>
        //private RequesterServiceMachineClientTestFixtureFactory CreateRequesterServiceMachineClientTestFixtureFactory()
        //{
        //    ProvideAuthorizationContext(new AuthorizationContext
        //    {
        //        AccessLevel = AccessLevel.Person,
        //        Actor = new AuthorizationActor
        //        {
        //            Id = "TestActor",
        //            Type = ActorType.User,
        //            DisplayName = "TestActorDisplayName"
        //        }
        //    });
        //    IAuthorizationContextProvider context = GetService<IAuthorizationContextProvider>();
        //    PlatformMachineAuthenticationService = Substitute.For<IPlatformMachineAuthenticationService>();
        //    RequesterServiceMachineClientTestFixtureFactory? requesterServiceMachineClientTestFixtureFactory =
        //        new(HttpClient, context, PlatformMachineAuthenticationService);
        //    return requesterServiceMachineClientTestFixtureFactory;
        //}

        /// <summary>
        /// This method is used for a client to machine testing.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="registerOwnUserClientSdkAction"></param>
        protected void ConfigureUserClientSdkForTesting(IServiceCollection services, Action registerOwnUserClientSdkAction)
        {
            services.AddSingleton((Func<IServiceProvider, IRequesterServiceFactory>)((sp) => CreateRequesterServiceUserClientTestFixtureFactory()));
            services.AddUserClientSdkDependencies();
            registerOwnUserClientSdkAction();
        }

        private RequesterServiceUserClientTestFixtureFactory CreateRequesterServiceUserClientTestFixtureFactory()
        {
            ProvideAuthorizationContext(new AuthorizationContext
            {
                AccessLevel = AccessLevel.Person,
                Actor = new AuthorizationActor
                {
                    Id = "TestActor",
                    Type = ActorType.User,
                    DisplayName = "TestActorDisplayName"
                }
            });
            RequesterServiceUserClientTestFixtureFactory? requesterServiceUserClientTestFixtureFactory = new(HttpClient);
            return requesterServiceUserClientTestFixtureFactory;
        }

        private HttpClient CreateHttpClient()
        {
            HttpClient httpClient = _webApplicationFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            return httpClient;
        }

        public T GetService<T>()
            where T : class
        {
            return ServiceProvider.GetRequiredService<T>();
        }

        public void ProvideAuthorizationContext(AuthorizationContext authorizationContext)
        {
            _acceptanceTestAuthorizationContextAccessor.AuthorizationContext = authorizationContext;
        }

        public async Task AddRootEntityToDatabaseAsync<TRootEntity>(TRootEntity rootEntity)
            where TRootEntity : RootEntity
        {
            await InsertRootEntityIntoDatabaseAsync(rootEntity);
        }

        private async Task InsertRootEntityIntoDatabaseAsync<TRootEntity>(TRootEntity rootEntity)
            where TRootEntity : RootEntity
        {
            SetUpHttpContextMock();
            IRelationalDbContext dbContext = GetDbContext();
            dbContext.Add(rootEntity);
            await dbContext.SaveChangesAsync();
        }

        private void SetUpHttpContextMock()
        {
            IHttpContextAccessor httpContextAccessor = GetService<IHttpContextAccessor>();
            httpContextAccessor.HttpContext = new HttpContextMock();
        }

        protected abstract IRelationalDbContext GetDbContext();

        public async Task RemoveRootEntityFromDatabaseAsync<TRootEntity>(TRootEntity rootEntity)
            where TRootEntity : RootEntity
        {
            bool doesRootEntityExist = HasAnyEntityInDatabase<TRootEntity>(e => e == rootEntity);
            if (doesRootEntityExist)
            {
                await DeleteRootEntityFromDatabaseAsync(rootEntity);
            }
        }

        public bool HasAnyEntityInDatabase<TEntity>(Func<TEntity, bool>? predicate = null)
            where TEntity : Entity
        {
            IRelationalDbContext dbContext = GetDbContext();
            IQueryable<TEntity> query = dbContext.QuerySet<TEntity>().IgnoreQueryFilters()!;
            if (predicate is not null)
            {
                query.Where(predicate);
            }
            return query.Any();
        }

        private async Task DeleteRootEntityFromDatabaseAsync<TRootEntity>(TRootEntity rootEntity)
            where TRootEntity : RootEntity
        {
            SetUpHttpContextMock();
            IRelationalDbContext dbContext = GetDbContext();
            dbContext.Remove(rootEntity);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddEntityToDatabaseAsync<TEntity>(TEntity entity)
            where TEntity : Entity
        {
            await AddEntityToDatabaseAsync<TEntity, RootEntity>(entity, null);
        }

        public async Task AddEntityToDatabaseAsync<TEntity, TRootEntity>(TEntity entity, TRootEntity? rootEntity)
            where TEntity : Entity
            where TRootEntity : RootEntity
        {
            await InsertEntityIntoDatabaseAsync(entity, rootEntity);
        }

        private async Task InsertEntityIntoDatabaseAsync<TEntity, TRootEntity>(TEntity entity, TRootEntity? rootEntity = null)
            where TEntity : Entity
            where TRootEntity : RootEntity
        {
            SetUpHttpContextMock();
            IRelationalDbContext dbContext = GetDbContext();
            if (rootEntity is not null)
            {
                dbContext.Attach(rootEntity);
            }
            dbContext.Add(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddValueObjectToDatabaseAsync<TValueObject>(TValueObject entity)
            where TValueObject : class
        {
            await InsertValueObjectIntoDatabaseAsync(entity);
        }

        private async Task InsertValueObjectIntoDatabaseAsync<TValueObject>(TValueObject entity)
            where TValueObject : class
        {
            SetUpHttpContextMock();
            IRelationalDbContext dbContext = GetDbContext();
            dbContext.Add(entity);
            await dbContext.SaveChangesAsync();
        }

        protected async Task RemoveEntity<TEntity>()
            where TEntity : Entity
        {
            ICollection<TEntity> entities = QueryEntitiesOnDatabase<TEntity>()
                .IgnoreQueryFilters()
                .ToList();
            await RemoveListOfEntities(entities);
        }

        private async Task RemoveListOfEntities<TEntity>(ICollection<TEntity> entities)
            where TEntity : Entity
        {
            foreach (TEntity entity in entities)
            {
                await RemoveEntityFromDatabaseAsync<TEntity>(entity);
            }
        }

        public async Task RemoveEntityFromDatabaseAsync<TEntity>(TEntity entity)
            where TEntity : Entity
        {
            await RemoveEntityFromDatabaseAsync<TEntity, RootEntity>(entity, null!);
        }

        public async Task RemoveEntityFromDatabaseAsync<TEntity, TRootEntity>(TEntity entity, TRootEntity rootEntity)
            where TEntity : Entity
            where TRootEntity : RootEntity
        {
            bool doesEntityExist = HasAnyEntityInDatabase<TEntity>(e => e == entity);
            if (doesEntityExist)
            {
                await DeleteEntityFromDatabaseAsync(entity, rootEntity);
            }
        }

        private async Task DeleteEntityFromDatabaseAsync<TEntity, TRootEntity>(TEntity entity, TRootEntity? rootEntity = null)
            where TEntity : Entity
            where TRootEntity : RootEntity
        {
            SetUpHttpContextMock();
            IRelationalDbContext dbContext = GetDbContext();
            if (rootEntity != null)
            {
                dbContext.Attach(rootEntity);
            }
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateRootEntityInDatabaseAsync<TRootEntity>(TRootEntity rootEntity)
            where TRootEntity : RootEntity
        {
            SetUpHttpContextMock();
            IRelationalDbContext dbContext = GetDbContext();
            dbContext.Update(rootEntity);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateEntityInDatabaseAsync<TEntity>(TEntity entity)
            where TEntity : Entity
        {
            await UpdateEntityInDatabaseAsync<TEntity, RootEntity>(entity, null);
        }

        public async Task UpdateEntityInDatabaseAsync<TEntity, TRootEntity>(TEntity entity, TRootEntity? rootEntity)
            where TEntity : Entity
            where TRootEntity : RootEntity
        {
            SetUpHttpContextMock();
            IRelationalDbContext dbContext = GetDbContext();
            if (rootEntity is not null)
            {
                dbContext.Attach(rootEntity);
            }
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> QueryEntitiesOnDatabase<TEntity>()
            where TEntity : Entity
        {
            IRelationalDbContext dbContext = GetDbContext();
            IQueryable<TEntity> query = dbContext.QuerySet<TEntity>();
            return query;
        }

        public IQueryable<TValueObject> QueryValueObjectOnDatabase<TValueObject>()
            where TValueObject : class
        {
            IRelationalDbContext dbContext = GetDbContext();
            IQueryable<TValueObject> query = dbContext.QuerySet<TValueObject>();
            return query;
        }

        public void Attach<TEntity>(TEntity entityToAttach)
            where TEntity : Entity
        {
            IRelationalDbContext dbContext = GetDbContext();
            dbContext.Attach(entityToAttach);
        }

        public void SetShadowPropertyValue<TEntity, TProperty>(
            TEntity entity,
            string propertyName,
            TProperty valueToSet)
            where TEntity : class
        {
            IRelationalDbContext dbContext = GetDbContext();
            dbContext.SetShadowPropertyValue(
                entity,
                propertyName,
                valueToSet);
        }

        public void Reset()
        {
            IRelationalDbContext dbContext = GetDbContext();
            dbContext.Reset();
        }

        public void Dispose()
        {
            GrpcHttpClient.Dispose();
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            HttpClient.Dispose();
            _webApplicationFactory.Dispose();
        }
    }
}
