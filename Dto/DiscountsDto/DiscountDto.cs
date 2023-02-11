using Ecommerce.Entities;
using ECommerce.Dto.Base;

namespace Dto.DiscountsDtos;

public class DiscountDto : BaseDto
{
    public double? Percent { get; set; }

    public int? Amount { get; set; }

    public string? Name { get; set; }

}
