namespace Ecommerce.Entities.ViewModel;

public class CategoryParentViewModel
{
    public int Id { get; set; }
    public int Depth { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public bool Checked { get; set; }
    public int DisplayOrder { get; set; }
    public Discount? Discount { get; set; }

    public List<CategoryParentViewModel> Children { get; set; }

    public static implicit operator CategoryParentViewModel(Category x)
    {
        return new CategoryParentViewModel
        {
            Id = x.Id,
            Depth = x.Depth,
            Name = x.Name,
            Path = x.Path,
            DisplayOrder = x.DisplayOrder,
            Discount = x.Discount
        };
    }
    public static implicit operator Category(CategoryParentViewModel x)
    {
        return new Category
        {
            Id = x.Id,
            Depth = x.Depth,
            Name = x.Name,
            Path = x.Path,
            DisplayOrder = x.DisplayOrder,
            Discount = x.Discount
        };
    }
}