using ECommerce.Dto.Base;

namespace ECommerce.Dto.PaymentMethodsDto;

public class PutPaymentMethodDto : BaseDto 
{
    public string? AccountNumber { get; set; }

    public string? BankName { get; set; }

    public string? BrunchName { get; set; }

    public string? BankCode { get; set; }
     
} 