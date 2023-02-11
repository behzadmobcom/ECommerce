using System.Transactions;

namespace Dto.PaymentMethodsDtos
{
    public class PaymentMethodsGetDto
    {
        public string AccountNumber { get; set; }

        public string BankName { get; set; }

        
        public string? BrunchName { get; set; }

        
        public string? BankCode { get; set; }

        public PaymentMethodStatus PaymentMethodStatus { get; set; }

        //ForeignKey
        public ICollection<Transaction>? Transactions { get; set; }
    }

    public enum PaymentMethodStatus
    {
        آنلاین = 0,
        واریز = 1,
        pos = 2
    }
}

