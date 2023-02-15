using ECommerce.Dto.Base;

namespace ECommerce.Dto.BrandsDto;

public class BrandDto_ : BaseDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? ImagePath { get; set; }

    //public Image? Image { get; set; }

    public string? Url { get; set; }

    //public ICollection<Product>? Products { get; set; }

}
