using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Message : BaseEntity
{
    [Display(Name = "نام")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 50 کاراکتر")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Name { get; set; }

    [Display(Name = "ایمیل")]
    [DataType(DataType.EmailAddress)]
    [StringLength(100)]
    [EmailAddress]
    public string? Email { get; set; }

    [Display(Name = "پیام")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 50 کاراکتر")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Body { get; set; }

    public int? UserId { get; set; }
    public DateTime DateTime { get; set; }
    public virtual User User { get; set; }
}