using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dto.ColorsDtos
{
    public class ColorsGetAllWithPaginationDto
    {
        public string Name { get; set; }

        public string ColorCode { get; set; }

        //ForeignKey
        [JsonIgnore] public ICollection<Price>? Prices { get; set; }
    }
}
