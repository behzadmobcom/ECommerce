using Ecommerce.Entities.ViewModel;

namespace Dto.BlogCategoriesDtos
{
    public class BlogCategoriesGetParentsDto
    {
        public int Id { get; set; }
        public int Depth { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public bool Checked { get; set; }
        public int DisplayOrder { get; set; }

        public List<CategoryParentViewModel> Children { get; set; }
    }
}
