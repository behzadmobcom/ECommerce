using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dto.KeywordsDtos
{
    public class KeywordsGetDto
    {
        public string KeywordText { get; set; }

        //ForeignKey
        [JsonIgnore] public ICollection<Blog>? Blogs { get; set; }

        [JsonIgnore] public virtual ICollection<Product>? Products { get; set; }
    }
}
