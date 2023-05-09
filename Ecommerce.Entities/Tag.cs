using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ecommerce.Entities;

public class Tag : BaseEntity
{
    [Display(Name = "تگ")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "حداقل 3 و حداکثر 50 کاراکتر")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string TagText { get; set; }

    public int TagId { get; set; }

    public List<string> TagsNames { get; set; }

    //ForeignKey
    [JsonIgnore] public ICollection<Blog>? Blogs { get; set; }

    [JsonIgnore] public virtual ICollection<Product>? Products { get; set; }
}