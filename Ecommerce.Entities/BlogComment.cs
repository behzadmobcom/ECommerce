using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Entities;

public class BlogComment : BaseEntity
{
    [Display(Name = "کامنت")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Text { get; set; }

    [Display(Name = "تایید شده")] public bool IsAccepted { get; set; }

    [Display(Name = "تاریخ")]
    //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateTime { get; set; }

    [Display(Name = "خوانده شده")] public bool IsRead { get; set; }

    [Display(Name = "پاسخ داده شده")] public bool IsAnswered { get; set; }

    [Display(Name = "ایمیل")]
    [StringLength(50)]
    public string? Email { get; set; }

    [Display(Name = "نام نویسنده")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 50 کاراکتر")]
    public string? Name { get; set; }

    //ForeignKey
    public int? UserId { get; set; }
    public User? User { get; set; }

    public int? AnswerId { get; set; }
    public BlogComment? Answer { get; set; }

    public int? BlogId { get; set; }
    public Blog? Blog { get; set; }

    public int? EmployeeId { get; set; }

    [Display(Name = "پاسخ دهنده")] public Employee? Employee { get; set; }
}