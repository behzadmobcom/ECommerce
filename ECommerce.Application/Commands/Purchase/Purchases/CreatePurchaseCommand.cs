using Ecommerce.Entities;

namespace ECommerce.Application.Commands.Purchase.Purchases;

public class CreatePurchaseCommand
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int DiscountAmount { get; set; }
    public ushort Quantity { get; set; }
    public bool IsColleague { get; set; }
    public int PriceId { get; set; }
    public int? DiscountId { get; set; }
}