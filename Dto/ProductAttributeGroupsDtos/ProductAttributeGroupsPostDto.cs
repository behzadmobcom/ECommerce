using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ProductAttributeGroupsDtos
{
    public class ProductAttributeGroupsPostDto
    {
        public string Name { get; set; }

        //ForeignKey

        //public ICollection<Category> Categories { get; set; }

        public List<ProductAttribute>? Attribute { get; set; } = new();
        public ICollection<Product>? Products { get; set; }
    }
}
