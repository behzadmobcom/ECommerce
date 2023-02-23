using ECommerce.Dto.Base;

namespace ECommerce.Dto.PaymentMethodsDto;

public class GetPaymentMethodDto : BaseDto
{
    public string? AccountNumber { get; set; }

    public string? BankName { get; set; }
     
}