using ECommerce.Dto.Base;

namespace ECommerce.Dto.CitiesDto;

public class CityDto_ : BaseDto
{
    public string? Name { get; set; }

    //public ICollection<SendInformation>? SendInformation { get; set; }

    //public ICollection<User>? User { get; set; }

    public int? StateId { get; set; }

    //public State? State { get; set; }
}
