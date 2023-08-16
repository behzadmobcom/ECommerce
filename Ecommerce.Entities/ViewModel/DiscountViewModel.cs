namespace Ecommerce.Entities.ViewModel;

public class DiscountViewModel
{
    public double? Percent { get; set; }
    public int? Amount { get; set; }
    public string Name { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? MaxAmount { get; set; }
    public int? MinOrder { get; set; }
    public int? MaxOrder { get; set; }
    public bool IsActive { get; set; } = false;
    public string Code { get; set; }
    public int? CouponQty { get; set; }

    //ForeignKey
    public List<int>? ProductsId { get; set; } = new List<int>();
    public List<Product>? Products { get; set; }
    public List<int>? PricesId { get; set; } = new List<int>();
    public List<Price>? Prices { get; set; }
    public List<int>? CategoriesId { get; set; } = new List<int>();
    public List<Category>? Categories { get; set; }
    public List<int>? PurchaseOrdersId { get; set; } = new List<int>();
    public List<PurchaseOrder>? PurchaseOrders { get; set; }
    public List<int>? PurchaseOrderDetailsId { get; set; } = new List<int>();
    public List<PurchaseOrderDetail>? PurchaseOrderDetails { get; set; }

    public static implicit operator DiscountViewModel(Discount x)
    {
        return new DiscountViewModel
        {
            Percent = x.Percent,
            Amount = x.Amount,
            Name = x.Name,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            MaxAmount = x.MaxAmount,
            MinOrder = x.MinOrder,
            MaxOrder = x.MaxOrder,
            IsActive = x.IsActive,
            Code = x.Code,
            CouponQty = x.CouponQty,
            ProductsId = x.Products.Select(x => x.Id).ToList(),
            PricesId = x.Prices.Select(x => x.Id).ToList(),
            CategoriesId = x.Categories.Select(x => x.Id).ToList(),
            PurchaseOrdersId = x.PurchaseOrders.Select(x => x.Id).ToList(),
            PurchaseOrderDetailsId = x.PurchaseOrderDetails.Select(x => x.Id).ToList(),
            Products = x.Products.ToList(),
            Prices = x.Prices.ToList(),
            Categories = x.Categories.ToList(),
            PurchaseOrders = x.PurchaseOrders.ToList(),
            PurchaseOrderDetails = x.PurchaseOrderDetails.ToList()
        };
    }
    public static implicit operator Discount(DiscountViewModel x)
    {
        return new Discount
        {
            Percent = x.Percent,
            Amount = x.Amount,
            Name = x.Name,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            MaxAmount = x.MaxAmount,
            MinOrder = x.MinOrder,
            MaxOrder = x.MaxOrder,
            IsActive = x.IsActive,
            Code = x.Code,
            CouponQty = x.CouponQty,
            Products = x.Products?.ToList(),
            Prices = x.Prices?.ToList(),
            Categories = x.Categories?.ToList(),
            PurchaseOrders = x.PurchaseOrders?.ToList(),
            PurchaseOrderDetails = x.PurchaseOrderDetails?.ToList()
        };
        }
}
