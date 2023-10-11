using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ecommerce.Entities;

public class Unit : RootEntity
{
    [Display(Name = "نام واحد")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "حداقل 3 و حداکثر 30 کاراکتر")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Name { get; set; }

    [Display(Name = "تعداد در واحد")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public double Few { get; set; }

    [Display(Name = "کد در هلو")] public int? UnitCode { get; set; }

    [Display(Name = "عیار")] public double? assay { get; set; }

    [Display(Name = "وزن واحد")] public short? UnitWeight { get; set; }

    //ForeignKey
    [JsonIgnore] public virtual ICollection<Price>? Prices { get; set; }

    public int? HolooCompanyId { get; set; } = 1;

    public virtual HolooCompany? HolooCompany { get; set; }
}