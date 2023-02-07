using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dto.ColorsDtos
{
    public class ColorsGetAllDto
    {
        public string Name { get; set; }

        public string ColorCode { get; set; }

        //ForeignKey
        [JsonIgnore] public ICollection<Price>? Prices { get; set; }
    }
}
