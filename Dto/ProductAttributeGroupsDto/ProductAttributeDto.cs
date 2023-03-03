using ECommerce.Dto.Base;

namespace ECommerce.Dto.ProductAttributeGroupsDto;

public class ProductAttributeDto : BaseDto
{
    public List<ProductAttributeValueDto>? AttributeValue { get; set; }

    public string? Title { get; set; }

}
