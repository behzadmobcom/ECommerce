using Ecommerce.Entities;

namespace Dto.ProductAttributeGroupsDtos
{
    public class ProductAttributeGroupsGetByProductIdDto
    {
        public string Name { get; set; }

        //ForeignKey

        //public ICollection<Category> Categories { get; set; }

        public List<ProductAttribute>? Attribute { get; set; } = new();
        public ICollection<Product>? Products { get; set; }
    }
}
