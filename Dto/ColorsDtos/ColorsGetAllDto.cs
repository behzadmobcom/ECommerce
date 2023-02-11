using Ecommerce.Entities;

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
