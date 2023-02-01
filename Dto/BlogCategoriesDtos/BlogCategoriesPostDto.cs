using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dto.BlogCategoriesDtos
{
    public class BlogCategoriesPostDto
    {
        public string Name { get; set; }

        public int? Depth { get; set; } = 0;

        public string? Description { get; set; }

        //ForeignKey
        public int? ParentId { get; set; }
        public BlogCategory? Parent { get; set; }

        [JsonIgnore] public ICollection<BlogCategory>? BlogCategories { get; set; } = new List<BlogCategory>();

        public ICollection<Blog>? Blogs { get; set; }
    }
}
