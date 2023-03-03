using ECommerce.Dto.PurchaseOrdersDto.Enums;

namespace ECommerce.Dto.PurchaseOrdersDto;

public class GetPurchaseOrderWithIncludeByIdPurchaseOrderDto : PurchaseOrderDto
{ 

    public Status? Status { get; set; }  

    public int? DiscountAmount { get; set; } 
      
    public int UserId { get; set; }

    public UserDto? User { get; set; } 
     
     
}
 