using ECommerce.Dto.Base;

namespace ECommerce.Dto.CurrenciesDto;

public class CurrencyDto_ : BaseDto
{
    public string? Name { get; set; }
    public int Amount { get; set; }
    //public ICollection<Price>? Prices { get; set; }
}
