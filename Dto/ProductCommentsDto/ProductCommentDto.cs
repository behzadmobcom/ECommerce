using ECommerce.Dto.Base;

namespace ECommerce.Dto.ProductCommentsDto;

public class ProductCommentDto : BaseDto
{
        public string? Text { get; set; } 
        
        public string? Name { get; set; }

        public int? ProductId { get; set; }
        public ProductDto? Product { get; set; }

}
