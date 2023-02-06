using Ecommerce.Entities;
using ECommerce.Dto.BlogCategoriesDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dto.BlogsDtos
{
    public class GetBlogDto
    {
        
        public string? Text { get; set; }

        public string? Title { get; set; }

        public string? Summary { get; set; }

        public DateTime CreateDateTime { get; set; } = DateTime.Now;

        public DateTime EditDateTime { get; set; } = DateTime.Now;

        public DateTime PublishDateTime { get; set; } = DateTime.Now;

        public string? Url { get; set; }

        public int Like { get; set; }

        public int Dislike { get; set; }

        public int Visit { get; set; }

        //ForeignKey
        public int BlogAuthorId { get; set; }
        public BlogAuthorDto BlogAuthor { get; set; }

        public int BlogCategoryId { get; set; }
        public BlogCategory BlogCategory { get; set; }

        public ICollection<BlogComment>? BlogComments { get; set; }

        public virtual ICollection<Keyword>? Keywords { get; set; }

        public virtual ICollection<Tag>? Tags { get; set; }

        public Image? Image { get; set; }
    }
}
