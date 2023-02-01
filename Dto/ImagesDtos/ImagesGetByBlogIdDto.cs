using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dto.ImagesDtos
{
    public class ImagesGetByBlogIdDto
    {
        public string? Name { get; set; }

        public string Path { get; set; }

        public string Alt { get; set; }

        //ForeignKey
        public int? ProductId { get; set; }

        [JsonIgnore] public virtual Product? Product { get; set; }

        public int? BlogId { get; set; }
        public virtual Blog? Blog { get; set; }
    }
}
