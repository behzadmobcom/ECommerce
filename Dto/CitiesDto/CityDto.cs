using ECommerce.Dto.Base;

namespace ECommerce.Dto.CitiesDto;

public class CityDto : BaseDto
{
    public string? Name { get; set; }

    public int? StateId { get; set; }

}
