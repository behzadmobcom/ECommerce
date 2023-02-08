using Ecommerce.Entities;
using ECommerce.Dto.BlogsDto;

namespace Dto.BlogsDtos;

public class BlogDto_ : BlogDto
{
    public string? Summary { get; set; }

    public DateTime CreateDateTime { get; set; } = DateTime.Now;

    public string? Url { get; set; }

    public int BlogAuthorId { get; set; }
    public BlogAuthor? BlogAuthor { get; set; }

    public int BlogCategoryId { get; set; }
    public BlogCategory? BlogCategory { get; set; }

    public ICollection<BlogComment>? BlogComments { get; set; }

    public virtual ICollection<Keyword>? Keywords { get; set; }

    public virtual ICollection<Tag>? Tags { get; set; }

    public Image? Image { get; set; }

}
