﻿using System.Transactions;

namespace Dto.PaymentMethodsDtos
{
    public class PaymentMethodsGetByIdDto
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

