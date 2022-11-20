using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Entities;

public class ProductAttributeGroup : BaseEntity
{
    [StringLength(50, MinimumLength = 2, ErrorMessage = @"حداقل 2 و حداکثر 50 کاراکتر")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Name { get; set; }

    //ForeignKey

    //public ICollection<Category> Categories { get; set; }

    public List<ProductAttribute>? Attribute { get; set; } = new();
    public ICollection<Product>? Products { get; set; }
}