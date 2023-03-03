using ECommerce.Dto.ProductAttributesDto.Enums;

namespace ECommerce.Dto.ProductAttributesDto;

public class PostProductAttributeDto
{
    public string? Title { get; set; }

    public AttributeType AttributeType { get; set; }

    public int? AttributeGroupId { get; set; }

}
