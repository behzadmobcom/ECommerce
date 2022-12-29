using System.ComponentModel.DataAnnotations;
using Ecommerce.Entities.Helper;

namespace Ecommerce.Entities.ViewModel;

public class ProductViewModel
{
    public int Id { get; set; }

    [Display(Name = "نام کالا")]
    [StringLength(255, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 255 کاراکتر")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Name { get; set; }

    public string? ArticleCode { get; set; }
    public string? ArticleCodeCustomer { get; set; }
    public string? Description { get; set; }
    public string? Review { get; set; }
    public double Exist { get; set; } = 0;


    [Display(Name = "حداقل سفارش")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public int MinOrder { get; set; } = 1;

    public int? MaxOrder { get; set; }
    public double? MinInStore { get; set; } = 0;
    public int? ReorderingLevel { get; set; }
    public bool IsDiscontinued { get; set; }
    public bool IsActive { get; set; } = true;

    [Display(Name = "آدرس محصول در سایت")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Url { get; set; }

    //ForeignKey

    public int StoreId { get; set; } = 1;
    public int SupplierId { get; set; } = 1;
    public int? BrandId { get; set; }
    public ICollection<ProductAttribute>? Attributes { get; set; }
    public int HolooCompanyId { get; set; } = 1;
    public List<int> KeywordsId { get; set; }
    public List<Keyword>? Keywords { get; set; }
    public List<int> TagsId { get; set; }
    public List<Tag>? Tags { get; set; }
    public List<int> CategoriesId { get; set; } = new();
    public List<string>? CategoriesUrl { get; set; }

    //public List<int> ImagesId { get; set; }
    public ICollection<Price>? Prices { get; set; } = new List<Price>();

    public ICollection<Image>? Images { get; set; }

    //public List<Category> ProductCategories { get; set; }
    //public List<Keyword> Keywords { get; set; }
    //public List<Tag> Tags { get; set; }
    public Brand? Brand { get; set; }
    public Supplier? Supplier { get; set; }
    public Store? Store { get; set; }
    public List<Category>? ProductCategories { get; set; }

    public static implicit operator ProductViewModel(Product p)
    {
        return new ProductViewModel
        {
            Id = p.Id,
            Name = p.Name,
            //ArticleCode = p.ArticleCode,
            //ArticleCodeCustomer = p.ArticleCodeCustomer,
            Description = p.Description,
            //Exist = p.Exist,
            Review = p.Review,
            MinOrder = p.MinOrder,
            MaxOrder = p.MaxOrder,
            MinInStore = p.MinInStore,
            ReorderingLevel = p.ReorderingLevel,
            IsDiscontinued = p.IsDiscontinued,
            IsActive = p.IsActive,
            Url = p.Url,
            HolooCompanyId = p.HolooCompanyId,
            BrandId = p.BrandId,
            SupplierId = p.SupplierId,
            StoreId = p.StoreId,
            Images = p.Images.ToList(),
            TagsId = p.Tags.Select(x => x.Id).ToList(),
            KeywordsId = p.Keywords.Select(x => x.Id).ToList(),
            CategoriesId = p.ProductCategories.Select(x => x.Id).ToList(),
            Prices = p.Prices,
            //ProductCategories=p.ProductCategories.ToList(),
            Brand = p.Brand,
            Supplier = p.Supplier,
            Store = p.Store,
            Tags = p.Tags.ToList(),
            Keywords = p.Keywords.ToList(),
            ProductCategories = p.ProductCategories.ToList()
        };
    }

    public static implicit operator Product(ProductViewModel p)
    {
        try
        {
            return new Product
            {
                Id = p.Id,
                Name = p.Name,
                //ArticleCode = p.ArticleCode,
                //ArticleCodeCustomer = p.ArticleCodeCustomer,
                Description = p.Description,
                //Exist = p.Exist,
                Review = p.Review,
                MinOrder = p.MinOrder,
                MaxOrder = p.MaxOrder,
                MinInStore = p.MinInStore,
                ReorderingLevel = p.ReorderingLevel,
                IsDiscontinued = p.IsDiscontinued,
                IsActive = p.IsActive,
                Url = p.Url,
                HolooCompanyId = p.HolooCompanyId,
                BrandId = p.BrandId,
                SupplierId = p.SupplierId,
                StoreId = p.StoreId,
                Images = p.Images,
                Brand = p.Brand,
                Supplier = p.Supplier,
                Store = p.Store,
                Tags = p.Tags?.ToList(),
                Keywords = p.Keywords?.ToList(),
                ProductCategories = p.ProductCategories?.ToList()
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}

public class ProductIndexPageViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Price> Prices { get; set; }
    public double Stars { get; set; }
    public string ImagePath { get; set; }
    public string Alt { get; set; }
    public string? Description { get; set; }
    public string? Review { get; set; }
    public string Url { get; set; }
    public string Brand { get; set; }
    public ushort MaxOrder { get; set; }
    public string? TopCategory { get; set; }

    public static implicit operator ProductIndexPageViewModel(Product x)
    {
        var imagePath = "img/product";
        var imageName = "NoImage.png";
        var imageAlt = "NoImage";
        var brandName = x.Brand == null ? "برند متفرقه" : x.Brand.Name;
        //var stars = x.ProductUserRanks.Count > 0 ? x.ProductUserRanks.Sum(x => x.Stars) / x.ProductUserRanks.Count : 0;
        if (x.Images.Count > 0)
        {
            imagePath = x.Images.FirstOrDefault().Path;
            imageName = x.Images.FirstOrDefault().Name;
            imageAlt = x.Images.FirstOrDefault().Alt;
        }

        return new ProductIndexPageViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Review = x.Review,
            Prices = x.Prices,
            ImagePath = $"{imagePath}/{imageName}",
            Url = x.Url,
            Brand = brandName,
            Alt = imageAlt,
            Stars = x.Star,
            MaxOrder =Convert.ToUInt16(x.MaxOrder),
        };
    }
}

public class ProductModalViewModel
{
    public string Description { get; set; }
    public string Price { get; set; }
    public string Brand { get; set; }
    public string Name { get; set; }
    public string ImagePath { get; set; }
    public string Alt { get; set; }
    public string Url { get; set; }
    public double Exist { get; set; }
}

public class ProductCompareViewModel
{
    public string ArticleCode { get; set; }
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int PriceId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImagePath { get; set; }
    public string Alt { get; set; }
    public string? Description { get; set; }
    public string? Review { get; set; }
    public string Url { get; set; }
    public string Brand { get; set; }
    public virtual List<ProductAttributeGroup> AttributeGroupProducts { get; set; }

    public static implicit operator ProductCompareViewModel(Product x)
    {
        var imagePath = "img/product";
        var imageName = "NoImage.png";
        var imageAlt = "NoImage";
        var brandName = x.Brand == null ? "برند متفرقه" : x.Brand.Name;
        if (x.Images.Count > 0)
        {
            imagePath = x.Images.FirstOrDefault()?.Path;
            imageName = x.Images.FirstOrDefault()?.Name;
            imageAlt = x.Images.FirstOrDefault()?.Alt;
        }

        return new ProductCompareViewModel
        {
            //ArticleCode = x.ArticleCode,
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Review = x.Review,
            Price = !x.Prices.Any() ? 0 : x.Prices.FirstOrDefault(y => !y.IsColleague && y.MinQuantity == 1).Amount,
            ImagePath = $"{imagePath}/{imageName}",
            Url = x.Url,
            Brand = brandName,
            Alt = imageAlt,
            AttributeGroupProducts = x.AttributeGroupProducts.ToList()
        };
    }
}

public class ProductListFilteredViewModel
{
    public PaginationParameters PaginationParameters { get; set; }
    public long StartPrice { get; set; }
    public long EndPrice { get; set; }
    public bool? IsExist { get; set; }
    public bool IsWithoutBail { get; set; }
    public ProductSort ProductSort { get; set; }
    public List<int>? BrandsId { get; set; }
    public List<int>? StarsCount { get; set; }
    public List<int>? TagsId { get; set; }
}