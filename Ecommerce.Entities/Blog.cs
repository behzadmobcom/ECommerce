using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Blog : BaseEntity
{
    [Display(Name = "متن مقاله")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Text { get; set; }

    [Display(Name = "عنوان")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 50 کاراکتر")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Title { get; set; }

    [Display(Name = "خلاصه")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Summary { get; set; }

    [Display(Name = "تاریخ ایجاد")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public DateTime CreateDateTime { get; set; } = DateTime.Now;

    [Display(Name = "تاریخ آخرین ویرایش")] public DateTime EditDateTime { get; set; } = DateTime.Now;

    [Display(Name = "تاریخ انتشار")] public DateTime PublishDateTime { get; set; } = DateTime.Now;

    [Display(Name = "آدرس")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 50 کاراکتر")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Url { get; set; }

    [Display(Name = "پسند")] public int Like { get; set; }

    [Display(Name = "نپسند")] public int Dislike { get; set; }

    [Display(Name = "بازدید")] public int Visit { get; set; }

    //ForeignKey
    public int BlogAuthorId { get; set; }
    public BlogAuthor BlogAuthor { get; set; }

    public int BlogCategoryId { get; set; }
    public BlogCategory BlogCategory { get; set; }

    public ICollection<BlogComment>? BlogComments { get; set; }

    public virtual ICollection<Keyword>? Keywords { get; set; }

    public virtual ICollection<Tag>? Tags { get; set; }
}