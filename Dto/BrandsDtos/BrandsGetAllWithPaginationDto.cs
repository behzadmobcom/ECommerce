using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dto.BrandsDtos
{
    public class BrandsGetAllWithPaginationDto
    {
        public string Name { get; set; }

        public string EnglishName { get; set; }

        public string? ImagePath { get; set; }

        public string? Description { get; set; }

        //ForeignKey
        [JsonIgnore] public ICollection<Blog>? Blogs { get; set; }
    }
}
