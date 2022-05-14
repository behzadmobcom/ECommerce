using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities;

public class Currency : BaseEntity
{
    [Display(Name = "نام")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Name { get; set; }

    [Display(Name = "قیمت")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public int Amount { get; set; }

    [JsonIgnore] public ICollection<Price>? Prices { get; set; }
}