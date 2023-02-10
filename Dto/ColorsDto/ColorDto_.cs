using Ecommerce.Entities;
using ECommerce.Dto.Base;

namespace Dto.ColorsDtos;

public class ColorDto_ : BaseDto
{
    public string? Name { get; set; }

    public string? ColorCode { get; set; }

    public ICollection<Price>? Prices { get; set; }
}
