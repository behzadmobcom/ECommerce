using Ecommerce.Entities;

namespace Dto.ColorsDtos;

public class GetByIdColorDto : ColorDto
{
    public ICollection<Price>? Prices { get; set; }
}
