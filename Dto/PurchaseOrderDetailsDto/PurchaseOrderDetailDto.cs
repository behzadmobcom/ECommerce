using ECommerce.Dto.Base;

namespace ECommerce.Dto.PurchaseOrderDetailsDto;

public class PurchaseOrderDetailDto : BaseDto
{
    public string? Name { get; set; }
     
    public ushort Quantity { get; set; }
     
    public decimal UnitPrice { get; set; }
     
    public decimal SumPrice { get; set; }
      
    public int? DiscountAmount { get; set; }
      
}
