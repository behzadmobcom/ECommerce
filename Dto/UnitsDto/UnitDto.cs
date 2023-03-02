using ECommerce.Dto.Base;

namespace ECommerce.Dto.UnitsDto;

public class UnitDto : BaseDto
{
    public string? Name { get; set; }
   
    public double Few { get; set; }

    public double? Assay { get; set; }

    public short? UnitWeight { get; set; }
     
}
