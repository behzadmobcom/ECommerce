namespace ECommerce.Application.Commands.Purchase;

public class CreatePurchaseCommand
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public bool IsColleague { get; set; }
    public int PriceId { get; set; }
}