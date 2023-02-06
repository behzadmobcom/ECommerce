using Ecommerce.Entities;

namespace ECommerce.Dto.BlogsDto;

public class GetByIdBlogDto : BlogDto
{
    public string? Text { get; set; }   
    public string? Title { get; set; }   
    public string? Summary { get; set; }   
     
 
    public Image? Image { get; set; }  
}
