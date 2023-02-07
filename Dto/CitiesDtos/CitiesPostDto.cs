using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

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
