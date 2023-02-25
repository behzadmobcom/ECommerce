using ECommerce.Dto.Base;

namespace ECommerce.Dto.PaymentMethodsDto;

public class PaymentMethodDto : BaseDto
{
    public string? AccountNumber { get; set; }
     
    public string? BankName { get; set; }
     
    public string? BrunchName { get; set; }
     
    public string? BankCode { get; set; }

    public PaymentMethodStatus PaymentMethodStatus { get; set; }

    //public ICollection<Transaction>? Transactions { get; set; }
}

public enum PaymentMethodStatus
{
    آنلاین = 0,
    واریز = 1,
    pos = 2
}