using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities
{
    public class BlogCategory : BaseEntity
    {

        [Display(Name = "نام")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 50 کاراکتر")]
        [Required(ErrorMessage = @"{0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "عمق")] 
        public int? Depth { get; set; } = 0;

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        //ForeignKey
        public int? ParentId { get; set; }
        public BlogCategory Parent { get; set; }

        [JsonIgnore]
        public ICollection<BlogCategory>? BlogCategories { get; set; } = new List<BlogCategory>();

        public ICollection<Blog>? Blogs { get; set; }

    }
}
