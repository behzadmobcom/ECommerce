using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Entities;

public class Store : BaseEntity
{
    [Display(Name = "نام")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "حداقل 3 و حداکثر 50 کاراکتر")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Name { get; set; }

    [Display(Name = "آدرس")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "حداقل 3 و حداکثر 200 کاراکتر")]
    public string? Address { get; set; }
}