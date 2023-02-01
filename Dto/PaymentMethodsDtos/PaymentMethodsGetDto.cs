using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;

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

