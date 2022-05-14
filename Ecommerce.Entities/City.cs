using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities;

public class City : BaseEntity
{
    [Display(Name = "نام شهر")]
    [StringLength(30, ErrorMessage = @"حداکثر 30 کاراکتر")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Name { get; set; }

    //ForeignKey
    [JsonIgnore] public ICollection<SendInformation>? SendInformation { get; set; }

    [JsonIgnore] public ICollection<User>? User { get; set; }

    [Required] public int? StateId { get; set; }

    [JsonIgnore] public State? State { get; set; }
}