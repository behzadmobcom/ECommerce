using ECommerce.Dto.Base;

namespace ECommerce.Dto.ProductsDto;

public class ImageDto : BaseDto
{
    public string? Path { get; set; }

    public string? Name { get; set; }

    public string? Alt { get; set; }

}
