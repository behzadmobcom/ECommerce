using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;
using ECommerce.Services.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);
var _frontSetting = builder.Configuration.GetSection(nameof(FrontSetting)).Get<FrontSetting>();
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/");
    options.Conventions.AllowAnonymousToPage("/register");
    options.Conventions.AllowAnonymousToPage("/Shop");
    options.Conventions.AllowAnonymousToPage("/Login");
    options.Conventions.AllowAnonymousToPage("/Product");
    options.Conventions.AllowAnonymousToPage("/Privacy");
    options.Conventions.AllowAnonymousToPage("/Index");
    options.Conventions.AllowAnonymousToPage("/Faq");
    options.Conventions.AllowAnonymousToPage("/Error");
    options.Conventions.AllowAnonymousToPage("/Contact");
    options.Conventions.AllowAnonymousToPage("/Compare");
    options.Conventions.AllowAnonymousToPage("/AboutUs");
    options.Conventions.AllowAnonymousToPage("/Cart");
    options.Conventions.AllowAnonymousToPage("/melisuccess");
    options.Conventions.AllowAnonymousToPage("/Blog");
    options.Conventions.AllowAnonymousToPage("/BlogDetails");
    options.Conventions.AllowAnonymousToPage("/Rules");
});
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();


//builder.Services.AddMvc().AddRazorPagesOptions(options =>
//{
//    options.Conventions.AddPageRoute("/Coming-Soon", "");
//});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => { options.LoginPath = "/Login"; });

builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
//If using Kestrel:
builder.Services.Configure<KestrelServerOptions>(options => { options.AllowSynchronousIO = true; });


//If using IIS:
builder.Services.Configure<IISServerOptions>(options => { options.AllowSynchronousIO = true; });

//builder.Services.Configure<RouteOptions>(options =>
//{
//    options.LowercaseUrls = true;
//    options.LowercaseQueryStrings = true;
//    //options.AppendTrailingSlash = true;
//});

builder.Services.AddTransient(_ => new HttpClient {BaseAddress = new Uri(_frontSetting.BaseAddress)});
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<SmsIrSettings>(builder.Configuration.GetSection("SmsIr"));
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
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
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
builder.Services.AddScoped<IProductCommentService, ProductCommentService>();

#endregion

// Add services to the container.
var mvcBuilder =builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    mvcBuilder.AddRazorRuntimeCompilation();
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