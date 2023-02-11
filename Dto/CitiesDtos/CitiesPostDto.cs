using Ecommerce.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dto.CitiesDtos
{
    public class CitiesPostDto
    {
        public string Name { get; set; }

        //ForeignKey
        [JsonIgnore] public ICollection<SendInformation>? SendInformation { get; set; }

        [JsonIgnore] public ICollection<User>? User { get; set; }

        [Required] public int? StateId { get; set; }

        [JsonIgnore] public State? State { get; set; }
    }
}
