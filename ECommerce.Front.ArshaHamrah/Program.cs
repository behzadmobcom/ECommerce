using ECommerce.Services.IServices;
using ECommerce.Services.Services;
using Entities.Helper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using ECommerce.Services.IServices;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);
var _frontSetting = builder.Configuration.GetSection(nameof(FrontSetting)).Get<FrontSetting>();

builder.Services.AddRazorPages(options =>
{
    //options.RootDirectory = "/Pages";
    //options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
    //options.Conventions.AuthorizePage("/Contact");
    //options.Conventions.AuthorizeFolder("/");
    //options.Conventions.AllowAnonymousToPage("/Login");
    //options.Conventions.AllowAnonymousToFolder("/Private/PublicPages");
    //options.Conventions.AuthorizeAreaFolder("Admin", "/");
    //options.Conventions.AuthorizeAreaFolder("Admin", "/", "SuperAdmin");
    //options.Conventions.AuthorizeAreaFolder("Admin", "/");
    //options.Conventions.AuthorizeFolder("/Register");
    //options.Conventions.AuthorizeFolder("/");
    options.Conventions.AllowAnonymousToPage("/");
    options.Conventions.AllowAnonymousToPage("/Shop");
    options.Conventions.AllowAnonymousToPage("/Product");
    options.Conventions.AllowAnonymousToPage("/Error");
    options.Conventions.AllowAnonymousToPage("/Contact");
    options.Conventions.AllowAnonymousToPage("/Compare");
    options.Conventions.AllowAnonymousToPage("/Cart");
    options.Conventions.AllowAnonymousToPage("/AboutUs");
    options.Conventions.AllowAnonymousToPage("/Login");
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => { options.LoginPath = "/Login"; });
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
//If using Kestrel:
builder.Services.Configure<KestrelServerOptions>(options => { options.AllowSynchronousIO = true; });


//If using IIS:
builder.Services.Configure<IISServerOptions>(options => { options.AllowSynchronousIO = true; });

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
    //options.AppendTrailingSlash = true;
});

builder.Services.AddTransient(_ => new HttpClient {BaseAddress = new Uri(_frontSetting.BaseAddress)});
builder.Services.AddHttpContextAccessor();

#region DI

builder.Services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IBlogCommentService, BlogCommentService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IBlogCategoryService, BlogCategoryService>();
builder.Services.AddScoped<IBlogAuthorService, BlogAuthorService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<ICompareService, CompareService>();
builder.Services.AddScoped<ICookieService, CookieService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<IHolooAccountNumberService, HolooAccountNumberService>();
builder.Services.AddScoped<IHolooArticleService, HolooArticleService>();
builder.Services.AddScoped<IHolooMGroupService, HolooMGroupService>();
builder.Services.AddScoped<IHolooService, HolooService>();
builder.Services.AddScoped<IHolooSGroupService, HolooSGroupService>();
builder.Services.AddScoped<IHolooUnitService, HolooUnitService>();
builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IKeywordService, KeywordService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
builder.Services.AddScoped<IPriceService, PriceService>();
builder.Services.AddScoped<IProductAttributeGroupService, ProductAttributeGroupService>();
builder.Services.AddScoped<IProductAttributeService, ProductAttributeService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
builder.Services.AddScoped<ISendInformationService, SendInformationService>();
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<ISlideShowService, SlideShowService>();
builder.Services.AddScoped<IStarService, StarService>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWishListService, WishListService>();

#endregion

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();