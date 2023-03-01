using ECommerce.Dto.Base;

namespace ECommerce.Dto.ProductsDto;

public class PostProductDto : BaseDto
{
    public string? Name { get; set; } 

    public string? Description { get; set; } 

    public List<PriceDto>? Prices { get; set; } 

    public ICollection<ImageDto>? Images { get; set; } 
     
}
