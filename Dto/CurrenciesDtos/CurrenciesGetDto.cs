using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dto.CurrenciesDtos
{
    public class CurrenciesGetDto
    {
        
        public string Name { get; set; }
        public int Amount { get; set; }
        [JsonIgnore] public ICollection<Price>? Prices { get; set; }
    }
}
