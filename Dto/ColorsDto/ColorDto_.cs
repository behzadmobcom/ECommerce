using ECommerce.Dto.Base;

namespace ECommerce.Dto.ColorsDto;

public class ColorDto_ : BaseDto
{
    public string? Name { get; set; }

    public string? ColorCode { get; set; }

    //public ICollection<Price>? Prices { get; set; }
}
