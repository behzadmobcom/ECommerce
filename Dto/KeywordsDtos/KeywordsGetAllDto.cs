using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
