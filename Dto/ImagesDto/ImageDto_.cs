using ECommerce.Dto.Base;

namespace ECommerce.Dto.ImagesDtos;

public class ImageDto_ : BaseDto
{
    public string? Name { get; set; }

    public string? Path { get; set; }

    public string? Alt { get; set; }

    public int? ProductId { get; set; }
    public virtual ProductDto? Product { get; set; }

    public int? BlogId { get; set; }
    public virtual BlogDto? Blog { get; set; }
}
