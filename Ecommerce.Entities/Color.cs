using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities
{
    public class Color : BaseEntity
    {

        [Display(Name = "نام رنگ")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = @"حداقل 2 و حداکثر 50 کاراکتر")]
        [Required(ErrorMessage = @"{0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "کد رنگ")]
        [StringLength(7)]
        [Required(ErrorMessage = @"{0} را وارد کنید")]
        public string ColorCode { get; set; }
        //ForeignKey
        [JsonIgnore]
        public ICollection<Price>? Prices { get; set; }
    }
}
