using ECommerce.Dto.Base;

namespace ECommerce.Dto.PurchaseOrdersDto;

public class PurchaseOrderDto : BaseDto
{ 
    public DateTime CreationDate { get; set; }  
     
    public ICollection<PurchaseOrderDetailDto>? PurchaseOrderDetails { get; set; }

}
