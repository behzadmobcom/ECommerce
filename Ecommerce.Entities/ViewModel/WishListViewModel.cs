namespace Ecommerce.Entities.ViewModel;

public class WishListViewModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int PriceId { get; set; }
    public Price Price { get; set; }=new();
    public string? Url { get; set; }
    public string? Name { get; set; }
    public string? Brand { get; set; }
    public string? ImagePath { get; set; }
    public string? Alt { get; set; }
    public string? Color { get; set; }
    public string? Grade { get; set; }
    public double Exist { get; set; }
    public string? StoreStatus { get; set; }
}