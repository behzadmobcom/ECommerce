using Ecommerce.Entities.Helper;

namespace Ecommerce.Entities.ViewModel;



public class TransactionFiltreViewModel
{
    public PaginationParameters? PaginationParameters { get; set; }
    public int? UserId { get; set; }
    //public DateTime CreationDate { get; set; }
    public TransactionSort? TransactionSort { get; set; }
    public DateTime? FromTransactionDate { get; set; }
    public DateTime? ToTransactionDate { get; set; }
    public decimal? MinimumAmount { get; set; }
    public decimal? MaximumAmount { get; set; }
    public PaymentMethodStatus? PaymentMethodStatus { get; set; }
}

