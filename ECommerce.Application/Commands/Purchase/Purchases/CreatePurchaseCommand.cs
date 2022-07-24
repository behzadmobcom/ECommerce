namespace ECommerce.Application.Commands.Purchase;

public class CreatePurchaseCommand
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public ushort Quantity { get; set; }
    public bool IsColleague { get; set; }
    public int PriceId { get; set; }
}