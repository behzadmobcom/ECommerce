using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dto.CategoriesDtos
{
    public class CategoriesGetDto
    {
       
        public string Name { get; set; }
        //  عمق گروه را مشخص می کند
        public int Depth { get; set; } = 0;

        // لیست پدر های گروه جاری را نشان می دهد مثلا 2/3/4/5
        public string Path { get; set; }

        public bool IsActive { get; set; } = true;

         public int DisplayOrder { get; set; } = 0;

        //ForeignKey
        public int? ParentId { get; set; }
        public Category? Parent { get; set; }

        [JsonIgnore] public ICollection<Category>? Categories { get; set; } = new List<Category>();

        [JsonIgnore] public ICollection<SlideShow>? SlideShows { get; set; }

        [JsonIgnore] public ICollection<Product>? Products { get; set; }

        //public ICollection<ProductAttributeGroup> AttributeGroups { get; set; }

        public int? DiscountId { get; set; }
        public Discount? Discount { get; set; }
    }
}
