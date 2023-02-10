using Ecommerce.Entities;

namespace Dto.ColorsDtos;

public class DeleteColorDto : ColorDto
{

    public ICollection<Price>? Prices { get; set; }
}
