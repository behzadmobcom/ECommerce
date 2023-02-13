using Ecommerce.Entities;
using System.Text.Json.Serialization;

namespace Dto.BlogAuthorsDto
{
    public class BlogAuthorsGetDto
    {
        public int CurrentPage { get; }
        public int TotalPages { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public string? Search { get; set; }

        
        public string Name { get; set; }

        
        public string EnglishName { get; set; }

        public string? ImagePath { get; set; }

         public string? Description { get; set; }

        //ForeignKey
        [JsonIgnore] public ICollection<Blog>? Blogs { get; set; }
    }
}
