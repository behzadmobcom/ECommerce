using Ecommerce.Entities;
using ECommerce.Dto.Base;
using ECommerce.Dto.BlogsDto;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Dto.BlogAuthorsDto;

public class BlogAuthorDto_ : BlogAuthorDto
{
    public string? EnglishName { get; set; }

    public string? ImagePath { get; set; }

    public string? Description { get; set; }

    public ICollection<BlogDto>? Blogs { get; set; }
}
