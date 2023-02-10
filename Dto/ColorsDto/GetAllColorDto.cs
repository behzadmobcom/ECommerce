using Ecommerce.Entities;

namespace Dto.ColorsDtos;

public class GetAllColorDto : ColorDto
{
    public ICollection<Price>? Prices { get; set; }
}
