using Ecommerce.Entities;
using System.Text.Json.Serialization;

namespace Dto.Brands
{
    public class BrandsGetAllDto
    {
        public string Name { get; set; }

        public string EnglishName { get; set; }

        public string? ImagePath { get; set; }

        public string? Description { get; set; }

        //ForeignKey
        [JsonIgnore] public ICollection<Blog>? Blogs { get; set; }
    }
}
