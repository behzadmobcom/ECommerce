using ECommerce.Dto.Base;

namespace ECommerce.Dto.ProductsDto;

public class DiscountDto : BaseDto
{
    public double? Percent { get; set; }

    public int? Amount { get; set; }
}
