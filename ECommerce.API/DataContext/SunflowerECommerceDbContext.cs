using API.Utilities;
using Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.DataContext;

public class SunflowerECommerceDbContext : IdentityDbContext<User, UserRole, int>
{
    private readonly IConfiguration _configRoot;
    private readonly IDataProtectionProvider _dataProtectionProvider;

    public SunflowerECommerceDbContext(DbContextOptions<SunflowerECommerceDbContext> options,
        IDataProtectionProvider dataProtectionProvider, IConfiguration configRoot) : base(options)
    {
        _dataProtectionProvider = dataProtectionProvider;
        _configRoot = configRoot;
    }


    public virtual DbSet<BlogAuthor> BlogAuthors { get; set; }
    public virtual DbSet<BlogCategory> BlogCategories { get; set; }
    public virtual DbSet<BlogComment> BlogComments { get; set; }
    public virtual DbSet<Blog> Blogs { get; set; }
    public virtual DbSet<Brand> Brands { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<Currency> Currencies { get; set; }
    public virtual DbSet<Color> Colors { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Discount> Discounts { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<HolooCompany> HolooCompanies { get; set; }
    public virtual DbSet<Image> Images { get; set; }
    public virtual DbSet<Keyword> Keywords { get; set; }
    public virtual DbSet<LoginHistory> LoginHistories { get; set; }
    public virtual DbSet<Message> Messages { get; set; }
    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
    public virtual DbSet<Price> Prices { get; set; }
    public virtual DbSet<Product?> Products { get; set; }
    public virtual DbSet<ProductAttributeGroup> ProductAttributeGroups { get; set; }
    public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }
    public virtual DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
    public virtual DbSet<ProductComment> ProductComments { get; set; }
    public virtual DbSet<ProductSellCount> ProductSellCounts { get; set; }
    public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    public virtual DbSet<PurchaseOrder?> PurchaseOrders { get; set; }
    public virtual DbSet<SendInformation> SendInformation { get; set; }
    public virtual DbSet<Setting> Settings { get; set; }
    public virtual DbSet<Shipping> Shipping { get; set; }
    public virtual DbSet<Size> Sizes { get; set; }
    public virtual DbSet<SlideShow> SlideShows { get; set; }
    public virtual DbSet<ProductUserRank> ProductUserRanks { get; set; }
    public virtual DbSet<State> States { get; set; }
    public virtual DbSet<Store> Stores { get; set; }
    public virtual DbSet<Supplier> Suppliers { get; set; }
    public virtual DbSet<Tag> Tags { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }
    public virtual DbSet<Unit> Units { get; set; }
    public virtual DbSet<WishList> WishLists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .Entity<City>()
            .HasOne(e => e.State)
            .WithMany(e => e.Cities)
            .OnDelete(DeleteBehavior.Restrict);

        #region نام استان و شهر

        modelBuilder.Entity<State>().HasData(
            new State {Id = 1, Name = "آذربايجان شرقي"},
            new State {Id = 2, Name = "آذربايجان غربي"},
            new State {Id = 3, Name = "اردبيل"},
            new State {Id = 4, Name = "اصفهان"},
            new State {Id = 5, Name = "البرز"},
            new State {Id = 6, Name = "ايلام"},
            new State {Id = 7, Name = "بوشهر"},
            new State {Id = 8, Name = "تهران"},
            new State {Id = 9, Name = "چهارمحال و بختياري"},
            new State {Id = 10, Name = "خراسان جنوبي"},
            new State {Id = 11, Name = "خراسان رضوي"},
            new State {Id = 12, Name = "خراسان شمالي"},
            new State {Id = 13, Name = "خوزستان"},
            new State {Id = 14, Name = "زنجان"},
            new State {Id = 15, Name = "سمنان"},
            new State {Id = 16, Name = "سيستان و بلوچستان"},
            new State {Id = 17, Name = "فارس"},
            new State {Id = 18, Name = "قزوين"},
            new State {Id = 19, Name = "قم"},
            new State {Id = 20, Name = "کردستان"},
            new State {Id = 21, Name = "کرمان"},
            new State {Id = 22, Name = "کرمانشاه"},
            new State {Id = 23, Name = "کهکيلويه و بويراحمد"},
            new State {Id = 24, Name = "گلستان"},
            new State {Id = 25, Name = "گيلان"},
            new State {Id = 26, Name = "لرستان"},
            new State {Id = 27, Name = "مازندران"},
            new State {Id = 28, Name = "مرکزي"},
            new State {Id = 29, Name = "هرمزگان"},
            new State {Id = 30, Name = "همدان"},
            new State {Id = 31, Name = "يزد"}
        );
        modelBuilder.Entity<City>().HasData(
            new City {Id = 1, Name = "اهر", StateId = 1},
            new City {Id = 2, Name = "عجبشير", StateId = 1},
            new City {Id = 3, Name = "آذر شهر", StateId = 1},
            new City {Id = 4, Name = "بناب", StateId = 1},
            new City {Id = 5, Name = "بستان آباد", StateId = 1},
            new City {Id = 6, Name = "چاراويماق", StateId = 1},
            new City {Id = 7, Name = "هشترود", StateId = 1},
            new City {Id = 8, Name = "هريس", StateId = 1},
            new City {Id = 9, Name = "جلفا", StateId = 1},
            new City {Id = 10, Name = "كليبر", StateId = 1},
            new City {Id = 11, Name = "خداآفرين", StateId = 1},
            new City {Id = 12, Name = "ملكان", StateId = 1},
            new City {Id = 13, Name = "مراغه", StateId = 1},
            new City {Id = 14, Name = "ميانه", StateId = 1},
            new City {Id = 15, Name = "مرند", StateId = 1},
            new City {Id = 16, Name = "اسكو", StateId = 1},
            new City {Id = 17, Name = "سراب", StateId = 1},
            new City {Id = 18, Name = "شبستر", StateId = 1},
            new City {Id = 19, Name = "تبريز", StateId = 1},
            new City {Id = 20, Name = "ورزقان", StateId = 1},
            new City {Id = 21, Name = "اروميه", StateId = 2},
            new City {Id = 22, Name = "نقده", StateId = 2},
            new City {Id = 23, Name = "ماكو", StateId = 2},
            new City {Id = 24, Name = "تكاب", StateId = 2},
            new City {Id = 25, Name = "خوي", StateId = 2},
            new City {Id = 26, Name = "مهاباد", StateId = 2},
            new City {Id = 27, Name = "سر دشت", StateId = 2},
            new City {Id = 28, Name = "چالدران", StateId = 2},
            new City {Id = 29, Name = "بوكان", StateId = 2},
            new City {Id = 30, Name = "مياندوآب", StateId = 2},
            new City {Id = 31, Name = "سلماس", StateId = 2},
            new City {Id = 32, Name = "شاهين دژ", StateId = 2},
            new City {Id = 33, Name = "پيرانشهر", StateId = 2},
            new City {Id = 34, Name = "اشنويه", StateId = 2},
            new City {Id = 35, Name = "چايپاره", StateId = 2},
            new City {Id = 36, Name = "پلدشت", StateId = 2},
            new City {Id = 37, Name = "شوط", StateId = 2},
            new City {Id = 38, Name = "اردبيل", StateId = 3},
            new City {Id = 39, Name = "سرعين", StateId = 3},
            new City {Id = 40, Name = "بيله سوار", StateId = 3},
            new City {Id = 41, Name = "پارس آباد", StateId = 3},
            new City {Id = 42, Name = "خلخال", StateId = 3},
            new City {Id = 43, Name = "مشگين شهر", StateId = 3},
            new City {Id = 44, Name = "نمين", StateId = 3},
            new City {Id = 45, Name = "نير", StateId = 3},
            new City {Id = 46, Name = "كوثر", StateId = 3},
            new City {Id = 47, Name = "گرمي", StateId = 3},
            new City {Id = 48, Name = "بوئين و مياندشت", StateId = 4},
            new City {Id = 49, Name = "مباركه", StateId = 4},
            new City {Id = 50, Name = "اردستان", StateId = 4},
            new City {Id = 51, Name = "خور و بيابانک", StateId = 4},
            new City {Id = 52, Name = "فلاورجان", StateId = 4},
            new City {Id = 53, Name = "فريدون شهر", StateId = 4},
            new City {Id = 54, Name = "كاشان", StateId = 4},
            new City {Id = 55, Name = "لنجان", StateId = 4},
            new City {Id = 56, Name = "گلپايگان", StateId = 4},
            new City {Id = 57, Name = "فريدن", StateId = 4},
            new City {Id = 58, Name = "نايين", StateId = 4},
            new City {Id = 59, Name = "اصفهان", StateId = 4},
            new City {Id = 60, Name = "نجف آباد", StateId = 4},
            new City {Id = 61, Name = "آران و بيدگل", StateId = 4},
            new City {Id = 62, Name = "چادگان", StateId = 4},
            new City {Id = 63, Name = "تيران و کرون", StateId = 4},
            new City {Id = 64, Name = "شهرضا", StateId = 4},
            new City {Id = 65, Name = "سميرم", StateId = 4},
            new City {Id = 66, Name = "خميني شهر", StateId = 4},
            new City {Id = 67, Name = "دهاقان", StateId = 4},
            new City {Id = 68, Name = "نطنز", StateId = 4},
            new City {Id = 69, Name = "برخوار", StateId = 4},
            new City {Id = 70, Name = "شاهين شهر و ميمه", StateId = 4},
            new City {Id = 71, Name = "خوانسار", StateId = 4},
            new City {Id = 72, Name = "ايلام", StateId = 6},
            new City {Id = 73, Name = "مهران", StateId = 6},
            new City {Id = 74, Name = "دهلران", StateId = 6},
            new City {Id = 75, Name = "آبدانان", StateId = 6},
            new City {Id = 76, Name = "چرداول", StateId = 6},
            new City {Id = 77, Name = "دره شهر", StateId = 6},
            new City {Id = 78, Name = "ايوان", StateId = 6},
            new City {Id = 79, Name = "بدره", StateId = 6},
            new City {Id = 80, Name = "سيروان", StateId = 6},
            new City {Id = 81, Name = "ملکشاهي", StateId = 6},
            new City {Id = 82, Name = "عسلويه", StateId = 7},
            new City {Id = 83, Name = "گناوه", StateId = 7},
            new City {Id = 84, Name = "دشتي", StateId = 7},
            new City {Id = 85, Name = "دشتستان", StateId = 7},
            new City {Id = 86, Name = "دير", StateId = 7},
            new City {Id = 87, Name = "بوشهر", StateId = 7},
            new City {Id = 88, Name = "كنگان", StateId = 7},
            new City {Id = 89, Name = "تنگستان", StateId = 7},
            new City {Id = 90, Name = "ديلم", StateId = 7},
            new City {Id = 91, Name = "جم", StateId = 7},
            new City {Id = 92, Name = "قرچك", StateId = 8},
            new City {Id = 93, Name = "پرديس", StateId = 8},
            new City {Id = 94, Name = "بهارستان", StateId = 8},
            new City {Id = 95, Name = "شميرانات", StateId = 8},
            new City {Id = 96, Name = "رباط كريم", StateId = 8},
            new City {Id = 97, Name = "فيروزكوه", StateId = 8},
            new City {Id = 98, Name = "تهران", StateId = 8},
            new City {Id = 99, Name = "ورامين", StateId = 8},
            new City {Id = 100, Name = "اسلامشهر", StateId = 8},
            new City {Id = 101, Name = "ري", StateId = 8},
            new City {Id = 102, Name = "پاكدشت", StateId = 8},
            new City {Id = 103, Name = "پيشوا", StateId = 8},
            new City {Id = 104, Name = "قدس", StateId = 8},
            new City {Id = 105, Name = "ملارد", StateId = 8},
            new City {Id = 106, Name = "شهريار", StateId = 8},
            new City {Id = 107, Name = "دماوند", StateId = 8},
            new City {Id = 108, Name = "بن", StateId = 9},
            new City {Id = 109, Name = "سامان", StateId = 9},
            new City {Id = 110, Name = "کيار", StateId = 9},
            new City {Id = 111, Name = "بروجن", StateId = 9},
            new City {Id = 112, Name = "اردل", StateId = 9},
            new City {Id = 113, Name = "شهركرد", StateId = 9},
            new City {Id = 114, Name = "فارسان", StateId = 9},
            new City {Id = 115, Name = "کوهرنگ", StateId = 9},
            new City {Id = 116, Name = "لردگان", StateId = 9},
            new City {Id = 117, Name = "داورزن", StateId = 11},
            new City {Id = 118, Name = "كلات", StateId = 11},
            new City {Id = 119, Name = "بردسكن", StateId = 11},
            new City {Id = 120, Name = "مشهد", StateId = 11},
            new City {Id = 121, Name = "نيشابور", StateId = 11},
            new City {Id = 122, Name = "سبزوار", StateId = 11},
            new City {Id = 123, Name = "كاشمر", StateId = 11},
            new City {Id = 124, Name = "گناباد", StateId = 11},
            new City {Id = 125, Name = "تربت حيدريه", StateId = 11},
            new City {Id = 126, Name = "خواف", StateId = 11},
            new City {Id = 127, Name = "تربت جام", StateId = 11},
            new City {Id = 128, Name = "تايباد", StateId = 11},
            new City {Id = 129, Name = "مه ولات", StateId = 11},
            new City {Id = 130, Name = "چناران", StateId = 11},
            new City {Id = 131, Name = "درگز", StateId = 11},
            new City {Id = 132, Name = "فيروزه", StateId = 11},
            new City {Id = 133, Name = "قوچان", StateId = 11},
            new City {Id = 134, Name = "سرخس", StateId = 11},
            new City {Id = 135, Name = "رشتخوار", StateId = 11},
            new City {Id = 136, Name = "بينالود", StateId = 11},
            new City {Id = 137, Name = "زاوه", StateId = 11},
            new City {Id = 138, Name = "جوين", StateId = 11},
            new City {Id = 139, Name = "بجستان", StateId = 11},
            new City {Id = 140, Name = "باخزر", StateId = 11},
            new City {Id = 141, Name = "فريمان", StateId = 11},
            new City {Id = 142, Name = "خليل آباد", StateId = 11},
            new City {Id = 143, Name = "جغتاي", StateId = 11},
            new City {Id = 144, Name = "خوشاب", StateId = 11},
            new City {Id = 145, Name = "زيرکوه", StateId = 10},
            new City {Id = 146, Name = "خوسف", StateId = 10},
            new City {Id = 147, Name = "درميان", StateId = 10},
            new City {Id = 148, Name = "قائنات", StateId = 10},
            new City {Id = 149, Name = "بشرويه", StateId = 10},
            new City {Id = 150, Name = "فردوس", StateId = 10},
            new City {Id = 151, Name = "بيرجند", StateId = 10},
            new City {Id = 152, Name = "نهبندان", StateId = 10},
            new City {Id = 153, Name = "سربيشه", StateId = 10},
            new City {Id = 154, Name = "سرايان", StateId = 10},
            new City {Id = 155, Name = "طبس", StateId = 11},
            new City {Id = 156, Name = "بجنورد", StateId = 12},
            new City {Id = 157, Name = "راز و جرگلان", StateId = 12},
            new City {Id = 158, Name = "اسفراين", StateId = 12},
            new City {Id = 159, Name = "جاجرم", StateId = 12},
            new City {Id = 160, Name = "شيروان", StateId = 12},
            new City {Id = 161, Name = "مانه و سملقان", StateId = 12},
            new City {Id = 162, Name = "گرمه", StateId = 12},
            new City {Id = 163, Name = "فاروج", StateId = 12},
            new City {Id = 164, Name = "کارون", StateId = 13},
            new City {Id = 165, Name = "حميديه", StateId = 13},
            new City {Id = 166, Name = "آغاجري", StateId = 13},
            new City {Id = 167, Name = "شوشتر", StateId = 13},
            new City {Id = 168, Name = "دشت آزادگان", StateId = 13},
            new City {Id = 169, Name = "اميديه", StateId = 13},
            new City {Id = 170, Name = "گتوند", StateId = 13},
            new City {Id = 171, Name = "شادگان", StateId = 13},
            new City {Id = 172, Name = "دزفول", StateId = 13},
            new City {Id = 173, Name = "رامشير", StateId = 13},
            new City {Id = 174, Name = "بهبهان", StateId = 13},
            new City {Id = 175, Name = "باوي", StateId = 13},
            new City {Id = 176, Name = "انديمشك", StateId = 13},
            new City {Id = 177, Name = "اهواز", StateId = 13},
            new City {Id = 178, Name = "انديکا", StateId = 13},
            new City {Id = 179, Name = "شوش", StateId = 13},
            new City {Id = 180, Name = "آبادان", StateId = 13},
            new City {Id = 181, Name = "هنديجان", StateId = 13},
            new City {Id = 182, Name = "خرمشهر", StateId = 13},
            new City {Id = 183, Name = "مسجد سليمان", StateId = 13},
            new City {Id = 184, Name = "ايذه", StateId = 13},
            new City {Id = 185, Name = "رامهرمز", StateId = 13},
            new City {Id = 186, Name = "باغ ملك", StateId = 13},
            new City {Id = 187, Name = "هفتکل", StateId = 13},
            new City {Id = 188, Name = "هويزه", StateId = 13},
            new City {Id = 189, Name = "ماهشهر", StateId = 13},
            new City {Id = 190, Name = "لالي", StateId = 13},
            new City {Id = 191, Name = "زنجان", StateId = 14},
            new City {Id = 192, Name = "ابهر", StateId = 14},
            new City {Id = 193, Name = "خدابنده", StateId = 14},
            new City {Id = 194, Name = "ماهنشان", StateId = 14},
            new City {Id = 195, Name = "خرمدره", StateId = 14},
            new City {Id = 196, Name = "ايجرود", StateId = 14},
            new City {Id = 197, Name = "طارم", StateId = 14},
            new City {Id = 198, Name = "سلطانيه", StateId = 14},
            new City {Id = 199, Name = "سمنان", StateId = 15},
            new City {Id = 200, Name = "شاهرود", StateId = 15},
            new City {Id = 201, Name = "گرمسار", StateId = 15},
            new City {Id = 202, Name = "سرخه", StateId = 15},
            new City {Id = 203, Name = "دامغان", StateId = 15},
            new City {Id = 204, Name = "آرادان", StateId = 15},
            new City {Id = 205, Name = "مهدي شهر", StateId = 15},
            new City {Id = 206, Name = "ميامي", StateId = 15},
            new City {Id = 207, Name = "زاهدان", StateId = 16},
            new City {Id = 208, Name = "بمپور", StateId = 16},
            new City {Id = 209, Name = "چابهار", StateId = 16},
            new City {Id = 210, Name = "خاش", StateId = 16},
            new City {Id = 211, Name = "سراوان", StateId = 16},
            new City {Id = 212, Name = "زابل", StateId = 16},
            new City {Id = 213, Name = "سرباز", StateId = 16},
            new City {Id = 214, Name = "قصر قند", StateId = 16},
            new City {Id = 215, Name = "نيكشهر", StateId = 16},
            new City {Id = 216, Name = "کنارک", StateId = 16},
            new City {Id = 217, Name = "ايرانشهر", StateId = 16},
            new City {Id = 218, Name = "زهک", StateId = 16},
            new City {Id = 219, Name = "سيب و سوران", StateId = 16},
            new City {Id = 220, Name = "ميرجاوه", StateId = 16},
            new City {Id = 221, Name = "دلگان", StateId = 16},
            new City {Id = 222, Name = "هيرمند", StateId = 16},
            new City {Id = 223, Name = "مهرستان", StateId = 16},
            new City {Id = 224, Name = "فنوج", StateId = 16},
            new City {Id = 225, Name = "هامون", StateId = 16},
            new City {Id = 226, Name = "نيمروز", StateId = 16},
            new City {Id = 227, Name = "شيراز", StateId = 17},
            new City {Id = 228, Name = "اقليد", StateId = 17},
            new City {Id = 229, Name = "داراب", StateId = 17},
            new City {Id = 230, Name = "فسا	", StateId = 17},
            new City {Id = 231, Name = "مرودشت", StateId = 17},
            new City {Id = 232, Name = "خرم بيد", StateId = 17},
            new City {Id = 233, Name = "آباده", StateId = 17},
            new City {Id = 234, Name = "كازرون", StateId = 17},
            new City {Id = 235, Name = "گراش", StateId = 17},
            new City {Id = 236, Name = "ممسني", StateId = 17},
            new City {Id = 237, Name = "سپيدان", StateId = 17},
            new City {Id = 238, Name = "لارستان", StateId = 17},
            new City {Id = 239, Name = "فيروز آباد", StateId = 17},
            new City {Id = 240, Name = "جهرم", StateId = 17},
            new City {Id = 241, Name = "ني ريز", StateId = 17},
            new City {Id = 242, Name = "استهبان", StateId = 17},
            new City {Id = 243, Name = "لامرد", StateId = 17},
            new City {Id = 244, Name = "مهر", StateId = 17},
            new City {Id = 245, Name = "پاسارگاد", StateId = 17},
            new City {Id = 246, Name = "ارسنجان", StateId = 17},
            new City {Id = 247, Name = "قيروكارزين", StateId = 17},
            new City {Id = 248, Name = "رستم", StateId = 17},
            new City {Id = 249, Name = "فراشبند", StateId = 17},
            new City {Id = 250, Name = "سروستان", StateId = 17},
            new City {Id = 251, Name = "زرين دشت", StateId = 17},
            new City {Id = 252, Name = "کوار", StateId = 17},
            new City {Id = 253, Name = "بوانات", StateId = 17},
            new City {Id = 254, Name = "خرامه", StateId = 17},
            new City {Id = 255, Name = "خنج", StateId = 17},
            new City {Id = 256, Name = "قزوين", StateId = 18},
            new City {Id = 257, Name = "تاكستان", StateId = 18},
            new City {Id = 258, Name = "آبيك", StateId = 18},
            new City {Id = 259, Name = "بوئين زهرا", StateId = 18},
            new City {Id = 260, Name = "البرز", StateId = 18},
            new City {Id = 261, Name = "آوج", StateId = 18},
            new City {Id = 262, Name = "قم", StateId = 19},
            new City {Id = 263, Name = "طالقان", StateId = 5},
            new City {Id = 264, Name = "اشتهارد", StateId = 5},
            new City {Id = 265, Name = "كرج", StateId = 5},
            new City {Id = 266, Name = "نظر آباد", StateId = 5},
            new City {Id = 267, Name = "ساوجبلاغ‎", StateId = 5},
            new City {Id = 268, Name = "فرديس", StateId = 5},
            new City {Id = 269, Name = "سنندج", StateId = 20},
            new City {Id = 270, Name = "ديواندره", StateId = 20},
            new City {Id = 271, Name = "بانه", StateId = 20},
            new City {Id = 272, Name = "بيجار", StateId = 20},
            new City {Id = 273, Name = "سقز", StateId = 20},
            new City {Id = 274, Name = "كامياران", StateId = 20},
            new City {Id = 275, Name = "قروه", StateId = 20},
            new City {Id = 276, Name = "مريوان", StateId = 20},
            new City {Id = 277, Name = "سروآباد", StateId = 20},
            new City {Id = 278, Name = "دهگلان‎", StateId = 20},
            new City {Id = 279, Name = "كرمان", StateId = 21},
            new City {Id = 280, Name = "راور", StateId = 21},
            new City {Id = 281, Name = "شهر بابک", StateId = 21},
            new City {Id = 282, Name = "انار", StateId = 21},
            new City {Id = 283, Name = "کوهبنان", StateId = 21},
            new City {Id = 284, Name = "رفسنجان", StateId = 21},
            new City {Id = 285, Name = "سيرجان", StateId = 21},
            new City {Id = 286, Name = "كهنوج", StateId = 21},
            new City {Id = 287, Name = "زرند", StateId = 21},
            new City {Id = 288, Name = "ريگان", StateId = 21},
            new City {Id = 289, Name = "بم", StateId = 21},
            new City {Id = 290, Name = "جيرفت", StateId = 21},
            new City {Id = 291, Name = "عنبرآباد", StateId = 21},
            new City {Id = 292, Name = "بافت", StateId = 21},
            new City {Id = 293, Name = "ارزوئيه", StateId = 21},
            new City {Id = 294, Name = "بردسير", StateId = 21},
            new City {Id = 295, Name = "فهرج", StateId = 21},
            new City {Id = 296, Name = "فارياب", StateId = 21},
            new City {Id = 297, Name = "منوجان", StateId = 21},
            new City {Id = 298, Name = "نرماشير", StateId = 21},
            new City {Id = 299, Name = "قلعه گنج", StateId = 21},
            new City {Id = 300, Name = "رابر", StateId = 21},
            new City {Id = 301, Name = "رودبار جنوب", StateId = 21},
            new City {Id = 302, Name = "كرمانشاه", StateId = 22},
            new City {Id = 303, Name = "اسلام آباد غرب", StateId = 22},
            new City {Id = 304, Name = "سر پل ذهاب", StateId = 22},
            new City {Id = 305, Name = "كنگاور", StateId = 22},
            new City {Id = 306, Name = "سنقر", StateId = 22},
            new City {Id = 307, Name = "قصر شيرين", StateId = 22},
            new City {Id = 308, Name = "گيلان غرب", StateId = 22},
            new City {Id = 309, Name = "هرسين", StateId = 22},
            new City {Id = 310, Name = "صحنه", StateId = 22},
            new City {Id = 311, Name = "پاوه", StateId = 22},
            new City {Id = 312, Name = "جوانرود", StateId = 22},
            new City {Id = 313, Name = "دالاهو", StateId = 22},
            new City {Id = 314, Name = "روانسر", StateId = 22},
            new City {Id = 315, Name = "ثلاث باباجاني", StateId = 22},
            new City {Id = 316, Name = "ياسوج", StateId = 23},
            new City {Id = 317, Name = "گچساران", StateId = 23},
            new City {Id = 318, Name = "دنا", StateId = 23},
            new City {Id = 319, Name = "کهگيلويه‎", StateId = 23},
            new City {Id = 320, Name = "لنده", StateId = 23},
            new City {Id = 321, Name = "بهمئي", StateId = 23},
            new City {Id = 322, Name = "باشت", StateId = 23},
            new City {Id = 323, Name = "بويراحمد", StateId = 23},
            new City {Id = 324, Name = "چرام", StateId = 23},
            new City {Id = 325, Name = "گرگان", StateId = 24},
            new City {Id = 326, Name = "آق قلا", StateId = 24},
            new City {Id = 327, Name = "گنبد كاووس", StateId = 24},
            new City {Id = 328, Name = "علي آباد", StateId = 24},
            new City {Id = 329, Name = "مينو دشت", StateId = 24},
            new City {Id = 330, Name = "تركمن", StateId = 24},
            new City {Id = 331, Name = "كردكوي", StateId = 24},
            new City {Id = 332, Name = "بندر گز", StateId = 24},
            new City {Id = 333, Name = "كلاله", StateId = 24},
            new City {Id = 334, Name = "آزاد شهر", StateId = 24},
            new City {Id = 335, Name = "راميان", StateId = 24},
            new City {Id = 336, Name = "گاليکش‎", StateId = 24},
            new City {Id = 337, Name = "مراوه تپه", StateId = 24},
            new City {Id = 338, Name = "گميشان", StateId = 24},
            new City {Id = 339, Name = "رشت", StateId = 25},
            new City {Id = 340, Name = "لنگرود", StateId = 25},
            new City {Id = 341, Name = "رودسر", StateId = 25},
            new City {Id = 342, Name = "طوالش", StateId = 25},
            new City {Id = 343, Name = "آستارا", StateId = 25},
            new City {Id = 344, Name = "آستانه اشرفيه", StateId = 25},
            new City {Id = 345, Name = "رودبار", StateId = 25},
            new City {Id = 346, Name = "فومن", StateId = 25},
            new City {Id = 347, Name = "صومعه سرا", StateId = 25},
            new City {Id = 348, Name = "بندرانزلي", StateId = 25},
            new City {Id = 349, Name = "رضوانشهر", StateId = 25},
            new City {Id = 350, Name = "ماسال", StateId = 25},
            new City {Id = 351, Name = "شفت", StateId = 25},
            new City {Id = 352, Name = "سياهكل", StateId = 25},
            new City {Id = 353, Name = "املش", StateId = 25},
            new City {Id = 354, Name = "لاهيجان", StateId = 25},
            new City {Id = 355, Name = "خرم آباد", StateId = 26},
            new City {Id = 356, Name = "دلفان", StateId = 26},
            new City {Id = 357, Name = "بروجرد", StateId = 26},
            new City {Id = 358, Name = "دورود", StateId = 26},
            new City {Id = 359, Name = "اليگودرز", StateId = 26},
            new City {Id = 360, Name = "ازنا", StateId = 26},
            new City {Id = 361, Name = "كوهدشت", StateId = 26},
            new City {Id = 362, Name = "سلسله", StateId = 26},
            new City {Id = 363, Name = "پلدختر", StateId = 26},
            new City {Id = 364, Name = "دوره", StateId = 26},
            new City {Id = 365, Name = "رومشکان", StateId = 26},
            new City {Id = 366, Name = "ساري", StateId = 27},
            new City {Id = 367, Name = "آمل", StateId = 27},
            new City {Id = 368, Name = "بابل", StateId = 27},
            new City {Id = 369, Name = "بابلسر", StateId = 27},
            new City {Id = 370, Name = "بهشهر", StateId = 27},
            new City {Id = 371, Name = "تنكابن", StateId = 27},
            new City {Id = 372, Name = "جويبار", StateId = 27},
            new City {Id = 373, Name = "چالوس", StateId = 27},
            new City {Id = 374, Name = "رامسر", StateId = 27},
            new City {Id = 375, Name = "سواد كوه", StateId = 27},
            new City {Id = 376, Name = "قائم شهر", StateId = 27},
            new City {Id = 377, Name = "نكا", StateId = 27},
            new City {Id = 378, Name = "نور", StateId = 27},
            new City {Id = 379, Name = "نوشهر", StateId = 27},
            new City {Id = 380, Name = "محمودآباد", StateId = 27},
            new City {Id = 381, Name = "فريدونکنار", StateId = 27},
            new City {Id = 382, Name = "عباس آباد", StateId = 27},
            new City {Id = 383, Name = "گلوگاه", StateId = 27},
            new City {Id = 384, Name = "مياندورود", StateId = 27},
            new City {Id = 385, Name = "سيمرغ", StateId = 27},
            new City {Id = 386, Name = "کلاردشت", StateId = 27},
            new City {Id = 387, Name = "سوادکوه شمالي", StateId = 27},
            new City {Id = 388, Name = "اراك", StateId = 28},
            new City {Id = 389, Name = "آشتيان", StateId = 28},
            new City {Id = 390, Name = "تفرش", StateId = 28},
            new City {Id = 391, Name = "خمين", StateId = 28},
            new City {Id = 392, Name = "دليجان", StateId = 28},
            new City {Id = 393, Name = "ساوه", StateId = 28},
            new City {Id = 394, Name = "زرنديه", StateId = 28},
            new City {Id = 395, Name = "محلات", StateId = 28},
            new City {Id = 396, Name = "شازند", StateId = 28},
            new City {Id = 397, Name = "فراهان", StateId = 28},
            new City {Id = 398, Name = "خنداب", StateId = 28},
            new City {Id = 399, Name = "کميجان", StateId = 28},
            new City {Id = 400, Name = "بندرعباس", StateId = 29},
            new City {Id = 401, Name = "قشم", StateId = 29},
            new City {Id = 402, Name = "بندر لنگه", StateId = 29},
            new City {Id = 403, Name = "بستك", StateId = 29},
            new City {Id = 404, Name = "حاجي آباد هرمزگان", StateId = 29},
            new City {Id = 405, Name = "رودان", StateId = 29},
            new City {Id = 406, Name = "ميناب", StateId = 29},
            new City {Id = 407, Name = "ابوموسي", StateId = 29},
            new City {Id = 408, Name = "جاسک", StateId = 29},
            new City {Id = 409, Name = "خمير", StateId = 29},
            new City {Id = 410, Name = "پارسيان", StateId = 29},
            new City {Id = 411, Name = "بشاگرد", StateId = 29},
            new City {Id = 412, Name = "سيريک", StateId = 29},
            new City {Id = 413, Name = "حاجي آباد", StateId = 29},
            new City {Id = 414, Name = "همدان", StateId = 30},
            new City {Id = 415, Name = "ملاير", StateId = 30},
            new City {Id = 416, Name = "تويسركان", StateId = 30},
            new City {Id = 417, Name = "نهاوند", StateId = 30},
            new City {Id = 418, Name = "كبودر اهنگ", StateId = 30},
            new City {Id = 419, Name = "رزن", StateId = 30},
            new City {Id = 420, Name = "اسدآباد", StateId = 30},
            new City {Id = 421, Name = "بهار", StateId = 30},
            new City {Id = 422, Name = "فامنين", StateId = 30},
            new City {Id = 423, Name = "يزد", StateId = 31},
            new City {Id = 424, Name = "تفت", StateId = 31},
            new City {Id = 425, Name = "اردكان", StateId = 31},
            new City {Id = 426, Name = "ابركوه", StateId = 31},
            new City {Id = 427, Name = "ميبد", StateId = 31},
            new City {Id = 428, Name = "بافق", StateId = 31},
            new City {Id = 429, Name = "صدوق", StateId = 31},
            new City {Id = 430, Name = "مهريز", StateId = 31},
            new City {Id = 431, Name = "خاتم", StateId = 31}
        );

        #endregion

        modelBuilder.Entity<UserRole>()
            .HasData(new UserRole {Id = 1, Name = "SuperAdmin", NormalizedName = "SUPERADMIN"});
        modelBuilder.Entity<UserRole>().HasData(new UserRole {Id = 2, Name = "Admin", NormalizedName = "ADMIN"});
        modelBuilder.Entity<UserRole>().HasData(new UserRole {Id = 4, Name = "Client", NormalizedName = "Client"});
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            UserName = "superadmin",
            NormalizedUserName = "SUPERADMIN",
            Email = "sayyah.alireza@gmail.com",
            NormalizedEmail = "SAYYAH.ALIREZA@GMAIL.COM",
            EmailConfirmed = true,
            PasswordHash = new PasswordHasher<User>().HashPassword(null, "!qa@ws#ed123"),
            SecurityStamp = string.Empty,
            AccessFailedCount = 0,
            FirstName = "Alireza",
            LastName = "Sayyah",
            PhoneNumber = "0911307006",
            PhoneNumberConfirmed = true,
            UserRoleId = 1,
            IsActive = true
        });
        //modelBuilder.Entity<User>().HasData(new User
        //{
        //    Id = 2,
        //    UserName = "sajjadnazmi",
        //    NormalizedUserName = "SAJJADNAZMI",
        //    Email = "sajjad.nazmi@gmail.com",
        //    NormalizedEmail = "SAJJAD.NAZMI@GMAIL.COM",
        //    EmailConfirmed = true,
        //    PasswordHash = new PasswordHasher<User>().HashPassword(null, "@grp_bolouri3395"),
        //    SecurityStamp = string.Empty,
        //    AccessFailedCount = 0,
        //    FirstName = "Sajjad",
        //    LastName = "Nazmi",
        //    PhoneNumber = "09119394726",
        //    PhoneNumberConfirmed = true,
        //    UserRoleId = 1,
        //    IsActive = true
        //});
        modelBuilder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int> {UserId = 1, RoleId = 1});
        modelBuilder.Entity<Setting>().HasData(new Setting
        {
            Id = 1,
            Name = "Currency",
            Value = "تومان"
        });
        modelBuilder.Entity<HolooCompany>().HasData(new HolooCompany
        {
            Id = 1,
            Name = "Holoo1",
            ConnectionString =
                "Server=.\\mssql2008r2;Database=Holoo1;Trusted_Connection=True;MultipleActiveResultSets=true;"
        });
        modelBuilder.Entity<Color>().HasData(new Color
        {
            Id = 1,
            Name = "بدون رنگ بندی",
            ColorCode = "#FFFFFF"
        });
        modelBuilder.Entity<Size>().HasData(new Size
        {
            Id = 1,
            Name = "بدون سایز بندی"
        });
        modelBuilder.Entity<Discount>().HasData(new Discount
        {
            Id = 1,
            Name = "بدون تخفیف",
            Code = "NoDiscount"
        });
        modelBuilder.Entity<Currency>().HasData(new Currency
        {
            Id = 1,
            Name = "تومان",
            Amount = 1
        });
        modelBuilder.Entity<Currency>().HasData(new Currency
        {
            Id = 2,
            Name = "دلار",
            Amount = 24000
        });
        modelBuilder.Entity<Store>().HasData(new Store
        {
            Id = 1,
            Name = "انبار پیش فرض"
        });
        modelBuilder.Entity<Supplier>().HasData(new Supplier
        {
            Id = 1,
            Name = "تامین کننده پیش فرض"
        });

        var entitiesAssembly = typeof(IBaseEntity<int>).Assembly;

        modelBuilder.RegisterAllEntities<IBaseEntity<int>>(entitiesAssembly);
        modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
        //modelBuilder.AddRestrictDeleteBehaviorConvention();
        modelBuilder.ConfigureIdentityTableName();
        modelBuilder.AddSequentialGuidForIdConvention();
        //modelBuilder.AddPluralizingTableNameConvention();
        modelBuilder.AddColumnProtector(_dataProtectionProvider, _configRoot);
    }
}