using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dto.CurrenciesDtos
{
    public class CurrenciesPostDto
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        [JsonIgnore] public ICollection<Price>? Prices { get; set; }
    }
}
