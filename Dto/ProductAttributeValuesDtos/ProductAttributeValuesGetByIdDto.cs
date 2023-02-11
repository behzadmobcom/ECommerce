using Ecommerce.Entities;
using System.Text.Json.Serialization;

namespace Dto.ProductAttributeValuesDtos
{
    public class ProductAttributeValuesGetByIdDto
    {
        public string? Value { get; set; }

        //ForeignKey
        public int? ProductAttributeId { get; set; }

        [JsonIgnore] public ProductAttribute? ProductAttribute { get; set; }

        public int? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
