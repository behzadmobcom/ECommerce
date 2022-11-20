namespace Ecommerce.Entities.ViewModel;

public class PageViewModel
{
    public int Page { get; set; } = 1;
    public int QuantityPerPage { get; set; } = 9;
    public string? SearchText { get; set; }
}