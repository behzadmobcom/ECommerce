using Ecommerce.Entities;
using ECommerce.Dto.Base;

namespace Dto.CurrenciesDtos;

public class CurrencyDto_ : BaseDto
{
    public string? Name { get; set; }
    public int Amount { get; set; }
    public ICollection<Price>? Prices { get; set; }
}
