using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities
{
    public class State : BaseEntity
    {

        [Display(Name = "نام استان")]
        [StringLength(30, ErrorMessage = @"حداکثر 30 کاراکتر")]
        public string Name { get; set; }

        //ForeignKey
        [JsonIgnore]
        public ICollection<SendInformation>? SendInformation { get; set; }
        [JsonIgnore]
        public ICollection<User>? Users { get; set; }

        public ICollection<City>? Cities { get; set; }
    }
}
