using Ecommerce.Entities;

namespace Dto.CurrenciesDtos;

public class CurrenciesPostDto
{
    public string? Name { get; set; }
    public int Amount { get; set; }
    public ICollection<Price>? Prices { get; set; }
}
