using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities
{
    public class ProductAttributeValue : BaseEntity
    {

        [Display(Name = "مقدار")]
        //[StringLength(500, MinimumLength = 1, ErrorMessage = @"حداکثر 500 کاراکتر")]
        //[Required(ErrorMessage = @"{0} را وارد کنید")]
        public string? Value { get; set; }

        //ForeignKey
        public int? ProductAttributeId { get; set; }
        [JsonIgnore]
        public ProductAttribute? ProductAttribute { get; set; }

        public int? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
