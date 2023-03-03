using ECommerce.Dto.Base;

namespace ECommerce.Dto.ProductAttributeGroupsDto;

public class AddWithAttributeValueProductAttributeGroupDto : ProductAttributeGroupDto
{ 
    public List<ProductAttributeDto>? Attribute { get; set; }
     

}
 