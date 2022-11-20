namespace Ecommerce.Entities.ViewModel;

public class SlideShowViewModel
{
    public int Id { get; set; }
    public int? ProductId { get; set; }
    public Product? Product { get; set; }
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
    public decimal? Price { get; set; }
    public int DisplayOrder { get; set; }

    public static implicit operator SlideShowViewModel(SlideShow s)
    {
        return new SlideShowViewModel
        {
            Id = s.Id,
            ProductId = s.ProductId,
            CategoryId = s.CategoryId,
            Title = s.Title,
            Description = s.Description,
            ImagePath = s.ImagePath,
            DisplayOrder = s.DisplayOrder,
            Price = s.Product?.Prices?.FirstOrDefault()?.Amount
        };
    }
}