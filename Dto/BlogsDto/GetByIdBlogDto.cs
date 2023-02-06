using Ecommerce.Entities;

namespace ECommerce.Dto.BlogsDto;

public class GetByIdBlogDto : BlogDto
{
    public string? Summary { get; set; }   
     
    public Image? Image { get; set; }  
}
