using Ecommerce.Entities;
using System.Text.Json.Serialization;

namespace Dto.CurrenciesDtos
{
    public class CurrenciesGetDto
    {
        
        public string Name { get; set; }
        public int Amount { get; set; }
        [JsonIgnore] public ICollection<Price>? Prices { get; set; }
    }
}
