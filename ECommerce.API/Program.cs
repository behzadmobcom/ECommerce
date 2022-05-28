using System.Text;
using System.Text.Json.Serialization;
using API.DataContext;
using API.Interface;
using API.Repository;
using ECommerce.API.Interface;
using ECommerce.API.Repository;
using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PersianTranslation.Identity;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false)
    .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
    .Enrich.FromLogContext()
    .WriteTo.Console(new CustomLogFormatter(),
        LogEventLevel.Warning)
    .WriteTo.MSSqlServer(
        builder.Configuration.GetConnectionString("SunflowerECommerce"),
        new MSSqlServerSinkOptions
        {
            TableName = "LogEvents",
            SchemaName = "EventLogging",
            AutoCreateSqlTable = true,
            BatchPostingLimit = 1000,
            BatchPeriod = new TimeSpan(00, 00, 30)
        }
        , restrictedToMinimumLevel: LogEventLevel.Warning)
    .ReadFrom.Configuration(hostingContext.Configuration)
);

var _siteSetting = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    );

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SunflowerECommerceDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("SunflowerECommerce")));
builder.Services.AddDbContext<HolooDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("HolooConnectionString")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddSwaggerGen(swagger =>
{
    //swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //swagger.IncludeXmlComments(xmlPath);
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddDataProtection();

builder.Services.AddSingleton(_siteSetting);

builder.Services.AddIdentity<User, UserRole>(identityOption =>
    {
        identityOption.User.RequireUniqueEmail = true;
        identityOption.Password.RequiredLength = 8;
        identityOption.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<SunflowerECommerceDbContext>()
    .AddDefaultTokenProviders()
    .AddErrorDescriber<PersianIdentityErrorDescriber>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = _siteSetting.IdentitySetting.Audience,
        ValidIssuer = _siteSetting.IdentitySetting.Issuer,
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_siteSetting.IdentitySetting.IdentitySecretKey))
    };
});

builder.Services.AddDistributedMemoryCache();

#region DI

builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
builder.Services.AddScoped<IBlogAuthorRepository, BlogAuthorRepository>();
builder.Services.AddScoped<IBlogCategoryRepository, BlogCategoryRepository>();
builder.Services.AddScoped<IBlogCommentRepository, BlogCommentRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IColorRepository, ColorRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IHolooCompanyRepository, HolooCompanyRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IKeywordRepository, KeywordRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
builder.Services.AddScoped<IPriceRepository, PriceRepository>();
builder.Services.AddScoped<IProductAttributeGroupRepository, ProductAttributeGroupRepository>();
builder.Services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
builder.Services.AddScoped<IProductAttributeValueRepository, ProductAttributeValueRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCommentRepository, ProductCommentRepository>();
builder.Services.AddScoped<IProductUserRankRepository, ProductUserRankRepository>();
builder.Services.AddScoped<IPurchaseOrderDetailRepository, PurchaseOrderDetailRepository>();
builder.Services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
builder.Services.AddScoped<ISendInformationRepository, SendInformationRepository>();
builder.Services.AddScoped<ISettingRepository, SettingRepository>();
builder.Services.AddScoped<IShippingRepository, ShippingRepository>();
builder.Services.AddScoped<ISizeRepository, SizeRepository>();
builder.Services.AddScoped<ISlideShowRepository, SlideShowRepository>();
builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWishListRepository, WishListRepository>();

builder.Services.AddScoped(typeof(IHolooRepository<>), typeof(HolooRepository<>));
builder.Services.AddScoped<IHolooAccountNumberRepository, HolooAccountNumberRepository>();
builder.Services.AddScoped<IHolooUnitRepository, HolooUnitRepository>();
builder.Services.AddScoped<IHolooArticleRepository, HolooArticleRepository>();
builder.Services.AddScoped<IHolooMGroupRepository, HolooMGroupRepository>();
builder.Services.AddScoped<IHolooSGroupRepository, HolooSGroupRepository>();
builder.Services.AddScoped<IHolooSanadRepository, HolooSanadRepository>();
builder.Services.AddScoped<IHolooSanadListRepository, HolooSanadListRepository>();
builder.Services.AddScoped<IHolooABailRepository, HolooABailRepository>();
builder.Services.AddScoped<IHolooFBailRepository, HolooFBailRepository>();
builder.Services.AddScoped<IHolooCustomerRepository, HolooCustomerRepository>();
builder.Services.AddScoped<IHolooSarfaslRepository, HolooSarfaslRepository>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}


if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger(c => { c.SerializeAsV2 = true; });

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
        c.RoutePrefix = string.Empty;
    });
}

using (var scope = app.Services.CreateScope())
{
    if (scope != null)
    {
        var context = scope.ServiceProvider.GetRequiredService<SunflowerECommerceDbContext>();
        context.Database.Migrate();
    }
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();