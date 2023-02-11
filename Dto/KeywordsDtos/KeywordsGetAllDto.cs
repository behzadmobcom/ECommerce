using Ecommerce.Entities;
using System.Text.Json.Serialization;

namespace Dto.KeywordsDtos
{
    public class KeywordsGetAllDto
    {
        public string KeywordText { get; set; }

        //ForeignKey
        [JsonIgnore] public ICollection<Blog>? Blogs { get; set; }

        [JsonIgnore] public virtual ICollection<Product>? Products { get; set; }
    }
}
