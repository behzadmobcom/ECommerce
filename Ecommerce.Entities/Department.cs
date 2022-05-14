using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Department : BaseEntity
{
    [Display(Name = "عنوان")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 30 کاراکتر")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Title { get; set; }

    [Display(Name = "مکان")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 30 کاراکتر")]
    public string? Location { get; set; }

    //ForeignKey
    public ICollection<Employee>? Employees { get; set; }
}