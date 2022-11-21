using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Entities;

public class Setting : BaseEntity
{
    [StringLength(50)]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Name { get; set; }

    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Value { get; set; }
}