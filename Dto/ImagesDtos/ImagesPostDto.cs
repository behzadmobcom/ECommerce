using Ecommerce.Entities;
using System.Text.Json.Serialization;

namespace Dto.ImagesDtos
{
    public class ImagesPostDto
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
