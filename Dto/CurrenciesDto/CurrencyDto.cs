using ECommerce.Dto.Base;

namespace Dto.CurrenciesDtos;

public class CurrencyDto : BaseDto
{
    public string? Name { get; set; }
    public int Amount { get; set; }

}
