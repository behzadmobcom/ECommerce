using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Dto.PaymentMethodsDtos
{
    public class PaymentMethodsPostDto
    {
        public string AccountNumber { get; set; }

        public string BankName { get; set; }


        public string? BrunchName { get; set; }


        public string? BankCode { get; set; }

        public PaymentMethodStatus PaymentMethodStatus { get; set; }

        //ForeignKey
        public ICollection<Transaction>? Transactions { get; set; }

    }
}
