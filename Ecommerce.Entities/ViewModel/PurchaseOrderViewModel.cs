namespace Entities.ViewModel;

public class PurchaseOrderViewModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string? Url { get; set; }
    public string? Name { get; set; }
    public string? Brand { get; set; }
    public string? ImagePath { get; set; }
    public string? Alt { get; set; }
    public int PriceId { get; set; }
    public Price Price { get; set; }
    public decimal PriceAmount { get; set; }
    public decimal SumPrice { get; set; }
    public ushort Quantity { get; set; }
    public double Exist { get; set; }

    public bool IsColleague { get; set; }

    //ForeignKey
    public int UserId { get; set; }
}