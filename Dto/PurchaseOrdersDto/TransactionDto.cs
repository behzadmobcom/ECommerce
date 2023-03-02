using ECommerce.Dto.Base;

namespace ECommerce.Dto.PurchaseOrdersDto;

public class TransactionDto : BaseDto
{
    public string? RefId { get; set; }
    public decimal Amount { get; set; }

}
