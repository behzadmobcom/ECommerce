using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ecommerce.Entities;

public class PurchaseOrderDetail : BaseEntity
{
    [Display(Name = "نام کالا")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Name { get; set; }

    [Display(Name = "تعداد")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public ushort Quantity { get; set; }

    [Display(Name = "قیمت واحد")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public decimal UnitPrice { get; set; }

    [Display(Name = "جمع")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public decimal SumPrice { get; set; }

    [Display(Name = "شناسه قیمت")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public int PriceId { get; set; }
    //[JsonIgnore] 
    public Price? Price { get; set; }

    [Display(Name = "تخفیف")]
    public int? DiscountAmount { get; set; }

    //ForeignKey
    public int? PurchaseOrderId { get; set; }

    [JsonIgnore] public PurchaseOrder? PurchaseOrder { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public int? DiscountId { get; set; }
    public Discount? Discount { get; set; }
}