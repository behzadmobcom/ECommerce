using System.ComponentModel.DataAnnotations;

namespace Entities;

public class ProductSellCount : BaseEntity
{
    public int ProductId { get; set; }

    [Display(Name = "تعداد فروش")] public int Count { get; set; }

    public Product? Product { get; set; }
}