namespace Entities.ViewModel;

public class SlideShowViewModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public string? Name { get; set; }
    public string Title { get; set; }
    public string? Url { get; set; }
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
            Title = s.Title,
            Description = s.Description,
            ImagePath = s.ImagePath,
            Name = s.Product?.Name,
            DisplayOrder = s.DisplayOrder,
            Price = s.Product?.Prices?.FirstOrDefault()?.Amount,
            Url = s.Product?.Url
        };
    }
}