using ECommerce.Dto.Base;

namespace ECommerce.Dto.CurrenciesDto;

public class CurrencyDto : BaseDto
{
    public string? Name { get; set; }
    public int Amount { get; set; }

}
