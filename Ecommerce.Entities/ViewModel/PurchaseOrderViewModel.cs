using Ecommerce.Entities.Helper;

namespace Ecommerce.Entities.ViewModel;

public class PurchaseOrderViewModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string? Url { get; set; }
    public string? Name { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string? ImagePath { get; set; }
    public string? Alt { get; set; }
    public int PriceId { get; set; }
    public Price Price { get; set; }
    public decimal PriceAmount { get; set; }
    public decimal SumPrice { get; set; }
    public ushort Quantity { get; set; }
    public double Exist { get; set; }
    public string ColorName { get; set; } = string.Empty;
    public bool IsColleague { get; set; }
    public Discount? Discount { get; set; }
    public int DiscountAmount { get; set; }
    public ICollection<Category> ProductCategories { get; set; }    

    //ForeignKey
    public int UserId { get; set; }
}

public class PurchaseFiltreOrderViewModel
{
    public PaginationParameters? PaginationParameters { get; set; }
    public int? UserId { get; set; }
    //public DateTime CreationDate { get; set; }
    public bool? IsPaied { get; set; }
    public PurchaseSort? PurchaseSort { get; set; }
    public DateTime? FromCreationDate { get; set; }
    public DateTime? ToCreationDate { get; set; }
    public int? StatusId { get; set; }
    public decimal? MinimumAmount { get; set; }
    public decimal? MaximumAmount { get; set; }
    public PaymentMethodStatus? PaymentMethodStatus { get; set; }
    public long? OrderId { get; set; }
}

public class PurchaseListViewModel
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public int Discount { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsPaied { get; set; }
    public string? Description { get; set; }
    public Status?  Status { get; set; }
    public SendInformation? SendInformation { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public List<PurchaseOrderDetail>? PurchaseOrderDetails  { get; set; }
    public int? UserId { get; set; }
    public string? UserName { get; set; }
    public string? FBailCode { get; set; }
    public long? OrderId { get; set; }
}