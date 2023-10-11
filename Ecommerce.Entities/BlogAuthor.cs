using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ecommerce.Entities;

public class BlogAuthor : RootEntity
{
    [Display(Name = "نام")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 50 کاراکتر")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Name { get; set; }

    [Display(Name = "نام انگلیسی")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 50 کاراکتر")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string EnglishName { get; set; }

    [Display(Name = "عکس")] public string? ImagePath { get; set; }

    [Display(Name = "توضیحات")] public string? Description { get; set; }

    //ForeignKey
    [JsonIgnore] public ICollection<Blog>? Blogs { get; set; }
}