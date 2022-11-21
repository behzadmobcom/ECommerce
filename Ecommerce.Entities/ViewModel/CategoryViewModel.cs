namespace Ecommerce.Entities.ViewModel;

public class CategoryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentId { get; set; }
    public Category Parent { get; set; }
    public List<int> Categories { get; set; }
    public List<int> ProductsId { get; set; }
    public int DisplayOrder { get; set; }
}

public class BlogCategoryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentId { get; set; }
    public Category Parent { get; set; }
    public List<int> Categories { get; set; }
    public List<int> BlogsId { get; set; }
    public int DisplayOrder { get; set; }
}