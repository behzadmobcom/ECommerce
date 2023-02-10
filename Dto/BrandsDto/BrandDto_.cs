using Ecommerce.Entities;
using ECommerce.Dto.Base;
using ECommerce.Dto.BlogsDto;
using System.Text.Json.Serialization;

namespace Dto.BrandsDtos;

public class BrandsDto_ : BaseDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? ImagePath { get; set; }

    public Image? Image { get; set; }

    public string? Url { get; set; }

    public ICollection<Product>? Products { get; set; }

}
