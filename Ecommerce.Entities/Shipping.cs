using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Shipping : BaseEntity
{
    [Display(Name = "نام")]
    [StringLength(300, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 300 کاراکتر")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Name { get; set; }

    //ForeignKey
    public ICollection<PurchaseOrder>? PurchaseOrder { get; set; }
}