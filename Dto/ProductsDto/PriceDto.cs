using ECommerce.Dto.Base;

namespace ECommerce.Dto.ProductsDto;

public class PriceDto : BaseDto
{
    public decimal Amount { get; set; }

    public bool IsColleague { get; set; }

    public double Exist { get; set; }

    public int? ColorId { get; set; } 
    public ColorDto? Color { get; set; }

    public int? DiscountId { get; set; }
    public DiscountDto? Discount { get; set; }
}
