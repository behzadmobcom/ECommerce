using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities;

public class Keyword : BaseEntity
{
    [Display(Name = "کلمه کلیدی")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 50 کاراکتر")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string KeywordText { get; set; }

    //ForeignKey
    [JsonIgnore] public ICollection<Blog>? Blogs { get; set; }

    [JsonIgnore] public virtual ICollection<Product>? Products { get; set; }
}