using ECommerce.Dto.Base;

namespace ECommerce.Dto.TransactionsDto;

public class TransactionDto : BaseDto
{
    public DateTime TransactionDate { get; set; } 
    public decimal Amount { get; set; }

    public string? RefId { get; set; }

    public double? PaymentId { get; set; }

    public int? SanadCode { get; set; }

    //public ICollection<PurchaseOrder>? PurchaseOrders { get; set; }

    //public int? UserId { get; set; }
    //public User? User { get; set; }

    //public int? PaymentMethodId { get; set; }
    //public PaymentMethod? PaymentMethod { get; set; }

    //public int? HolooCompanyId { get; set; } 
    //public HolooCompany? HolooCompany { get; set; }
}
