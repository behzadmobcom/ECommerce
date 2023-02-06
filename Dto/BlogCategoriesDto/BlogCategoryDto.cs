using Ecommerce.Entities;
using ECommerce.Dto.Base;
using ECommerce.Dto.BlogsDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ECommerce.Dto.BlogCategoriesDto
{
    internal class BlogCategoryDto : BaseDto
    {
        public string? Name { get; set; }

        public int? Depth { get; set; } = 0;

        public string? Description { get; set; }

        //ForeignKey
        public int? ParentId { get; set; }
        public BlogCategoryDto? Parent { get; set; }

        public ICollection<BlogCategoryDto>? BlogCategories { get; set; }  

        public ICollection<BlogDto>? Blogs { get; set; }
    }
}
