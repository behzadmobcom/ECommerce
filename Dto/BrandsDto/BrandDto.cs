using ECommerce.Dto.Base;

namespace Dto.BrandsDtos;

public class BrandDto : BaseDto
{
    public string? Name { get; set; }

    public string? Url { get; set; }

    public string? Description { get; set; }


    //public string? Description { get; set; }
    //public string? ImagePath { get; set; }
    //public Image? Image { get; set; }
    //public string? Url { get; set; }
    //public ICollection<ProductDto>? Products { get; set; }

}
