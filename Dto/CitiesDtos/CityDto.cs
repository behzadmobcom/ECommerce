using Ecommerce.Entities;
using ECommerce.Dto.Base;

namespace Dto.CitiesDtos;

public class CityDto : BaseDto
{
    public string? Name { get; set; }

    public int? StateId { get; set; }

}
