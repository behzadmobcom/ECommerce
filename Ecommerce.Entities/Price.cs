using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Entities.Helper;

namespace Entities;

public class Price : BaseEntity
{
    public enum HolooSellNumber
    {
        خالی = 0,
        Sel_Price = 1,
        Sel_Price2 = 2,
        Sel_Price3 = 3,
        Sel_Price4 = 4,
        Sel_Price5 = 5,
        Sel_Price6 = 6,
        Sel_Price7 = 7,
        Sel_Price8 = 8,
        Sel_Price9 = 9,
        Sel_Price10 = 10
    }

    [Display(Name = "قیمت")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public decimal Amount { get; set; }

    [Display(Name = "حداقل تعداد برای این قیمت")]
    public int MinQuantity { get; set; } = 1;

    [Display(Name = "حداکثر تعداد برای این قیمت")]
    public int MaxQuantity { get; set; }

    [Display(Name = "قیمت برای همکار")] public bool IsColleague { get; set; } = false;

    [Display(Name = "قیمت پیش فرض")] public bool IsDefault { get; set; } = false;

    public HolooSellNumber? SellNumber { get; set; }

    [StringLength(7)]
    [Display(Name = "کد دیتابیس کالا در هلو")]
    public string? ArticleCode { get; set; }

    [StringLength(50)]
    [Display(Name = "کد کالا در هلو")]
    public string? ArticleCodeCustomer { get; set; }

    [Display(Name = "موجودی")] public double Exist { get; set; } = 0;

    [Display(Name = "درجه")] public Grade Grade { get; set; }
    //ForeignKey

    public int ProductId { get; set; }

    [JsonIgnore] public Product? Product { get; set; }

    public int? UnitId { get; set; }
    public Unit? Unit { get; set; }

    public int? SizeId { get; set; }
    public Size? Size { get; set; }

    public int? ColorId { get; set; } = 1;
    public Color? Color { get; set; }

    public int? CurrencyId { get; set; }
    public Currency? Currency { get; set; }
}