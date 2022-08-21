using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities;

public class Product : BaseEntity
{
    [Display(Name = "نام کالا")]
    [StringLength(255, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 255 کاراکتر")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Name { get; set; }

    //[StringLength(7)]
    //[Display(Name = "کد دیتابیس کالا در هلو")]
    //public string? ArticleCode { get; set; }

    [Display(Name = "نمایش در صفحه اول")]
    public bool IsShowInIndexPage { get; set; }

    [Display(Name = "توضیحات")] public string? Description { get; set; }


    [Display(Name = "نقد و بررسی")] public string? Review { get; set; }
    //[Display(Name = "موجودی")]
    //public double Exist { get; set; } = 0;

    [Display(Name = "امتیاز")] public double Star { get; set; } = 5;

    [Display(Name = "حداقل سفارش")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public int MinOrder { get; set; } = 1;

    [Display(Name = "حداکثر سفارش")] public int? MaxOrder { get; set; }

    [Display(Name = "حداقل در انبار")] public double? MinInStore { get; set; } = 0;

    // وقتی به این تعداد رسید باید سفارش مجدد داده شود
    [Display(Name = "تعداد سفارش مجدد")] public int? ReorderingLevel { get; set; }

    [Display(Name = "توقف تولید")] public bool IsDiscontinued { get; set; }

    [Display(Name = "فعال")] public bool IsActive { get; set; }

    [Display(Name = "آدرس محصول در سایت")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Url { get; set; }

    //ForeignKey
    public ICollection<Category>? ProductCategories { get; set; }

    [JsonIgnore] public ICollection<Price>? Prices { get; set; } = new List<Price>();

    public int StoreId { get; set; } = 1;
    public Store? Store { get; set; }

    public int SupplierId { get; set; } = 1;
    public Supplier? Supplier { get; set; }

    public int? BrandId { get; set; }
    public Brand? Brand { get; set; }

    public ICollection<ProductAttributeValue>? AttributeValues { get; set; }

    public ICollection<ProductAttributeGroup>? AttributeGroupProducts { get; set; }

    //public int? DiscountId { get; set; }
    //public Discount? Discount { get; set; }

    public int HolooCompanyId { get; set; } = 1;
    public HolooCompany HolooCompany { get; set; }

    public ICollection<Keyword>? Keywords { get; set; }

    public ICollection<Tag>? Tags { get; set; }

    public ICollection<Image>? Images { get; set; }

    public ICollection<ProductUserRank>? ProductUserRanks { get; set; }

    [JsonIgnore] public ICollection<SlideShow>? SlideShows { get; set; }

    public ICollection<ProductComment>? ProductComments { get; set; }
}