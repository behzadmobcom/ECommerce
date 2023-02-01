using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dto.ProductAttributeValuesDtos
{
    public class ProductAttributeValuesGetDto
    {
        public string? Value { get; set; }

        //ForeignKey
        public int? ProductAttributeId { get; set; }

        [JsonIgnore] public ProductAttribute? ProductAttribute { get; set; }

        public int? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
