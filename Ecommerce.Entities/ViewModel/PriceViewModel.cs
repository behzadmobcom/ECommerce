using System.ComponentModel.DataAnnotations;
using Ecommerce.Entities.Helper;

namespace Ecommerce.Entities.ViewModel;

public class PriceViewModel
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int ProductId { get; set; }
    public string ColorName { get; set; }
    public int? ColorId { get; set; }
    public decimal Amount { get; set; }

    public static implicit operator PriceViewModel(Price x)
    {
        return new PriceViewModel
        {
            Id = x.Id,
            ProductName = x.Product.Name,
            ProductId = x.ProductId,
            ColorName = x.Color.Name,
            ColorId = x.Color.Id,
            Amount = x.Amount,
        };
    }
   
}