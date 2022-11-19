using System.ComponentModel.DataAnnotations;

namespace Entities;

public class SlideShow : BaseEntity
{
    [Display(Name = "عنوان")]
    [StringLength(50, ErrorMessage = @"حداکثر 50 کاراکتر")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Title { get; set; }

    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Description { get; set; }

    [Display(Name = "عکس")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string ImagePath { get; set; }

    [Display(Name = "ترتیب نمایش")] public int DisplayOrder { get; set; } = 0;

    //ForeignKey
    public int? ProductId { get; set; }
    public Product? Product { get; set; }
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
}