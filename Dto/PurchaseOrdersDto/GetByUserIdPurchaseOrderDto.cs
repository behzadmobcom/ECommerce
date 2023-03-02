namespace ECommerce.Dto.PurchaseOrdersDto;

public class GetByUserIdPurchaseOrderDto : PurchaseOrderDto
{
    public Guid OrderGuid { get; set; } 

    public long OrderId { get; set; } 

    public decimal Amount { get; set; } 
     
    public int? SendInformationId { get; set; }
    public SendInformationDto? SendInformation { get; set; } 

    public int? TransactionId { get; set; } 
    public TransactionDto? Transaction { get; set; } 
}
 