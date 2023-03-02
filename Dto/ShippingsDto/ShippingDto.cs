using ECommerce.Dto.Base;

namespace ECommerce.Dto.ShippingsDto;

public class ShippingDto : BaseDto
{
    public string? Name { get; set; }
    public ICollection<PurchaseOrderDto>? PurchaseOrder { get; set; }

}
