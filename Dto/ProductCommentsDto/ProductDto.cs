using ECommerce.Dto.Base;

namespace ECommerce.Dto.ProductCommentsDto;

public class ProductDto : BaseDto
{
    public string? Name { get; set; }
    public ICollection<ImageDto>? Images { get; set; }

}