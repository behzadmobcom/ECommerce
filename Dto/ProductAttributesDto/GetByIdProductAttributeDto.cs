using ECommerce.Dto.ProductAttributesDto.Enums;

namespace ECommerce.Dto.ProductAttributesDto;

public class GetByIdProductAttributeDto : ProductAttributeDto
{
    public AttributeType AttributeType { get; set; }

    public int? AttributeGroupId { get; set; }

}
