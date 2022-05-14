using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities;

public class Image : BaseEntity
{
    [Display(Name = "نام")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 50 کاراکتر")]
    public string? Name { get; set; }

    [Display(Name = "مسیر")]
    [StringLength(40)]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Path { get; set; }

    [Display(Name = "جایگزین عکس")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 50 کاراکتر")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Alt { get; set; }

    //ForeignKey
    public int? ProductId { get; set; }

    [JsonIgnore] public virtual Product? Product { get; set; }

    public int? BlogId { get; set; }
    public virtual Blog? Blog { get; set; }
}