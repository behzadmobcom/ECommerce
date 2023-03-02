using ECommerce.Dto.Base;

namespace ECommerce.Dto.ProductAttributeGroupsDto;

public class ProductAttributeGroupDto : BaseDto
{
    public string? Name { get; set; }
     
    public List<ProductAttributeDto>? Attribute { get; set; } = new();
     
}
