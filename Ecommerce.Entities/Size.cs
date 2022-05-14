using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities;

public class Size : BaseEntity
{
    [Display(Name = "سایز")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = @"حداقل 1 و حداکثر 50 کاراکتر")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Name { get; set; }

    public string? Description { get; set; }

    //ForeignKey
    [JsonIgnore] public ICollection<Price>? Prices { get; set; }
}