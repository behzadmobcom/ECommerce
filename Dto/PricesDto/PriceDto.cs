using ECommerce.Dto.Base;
using ECommerce.Dto.PricesDto.Enums;

namespace Ecommerce.Dto.PricesDto;

public class PriceDto : BaseDto
{

    public decimal Amount { get; set; }

    public int MinQuantity { get; set; }

    public int MaxQuantity { get; set; }

    public HolooSellNumber? SellNumber { get; set; }

    public string? ArticleCode { get; set; }

    public string? ArticleCodeCustomer { get; set; }

    public double Exist { get; set; }

    public Grade Grade { get; set; }

    public int ProductId { get; set; }
    public ProductDto? Product { get; set; }

    public int? UnitId { get; set; }
    public UnitDto? Unit { get; set; }

    public int? SizeId { get; set; }
    public SizeDto? Size { get; set; }

    public int? ColorId { get; set; }
    public ColorDto? Color { get; set; }

    public int? CurrencyId { get; set; }
    public CurrencyDto? Currency { get; set; }

}
