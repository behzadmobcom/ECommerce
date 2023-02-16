namespace ECommerce.Dto.DiscountsDto;

public class GetByIdDiscountDto : DiscountDto
{
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IsActive { get; set; } = false;

    public string? Code { get; set; }
     
}
