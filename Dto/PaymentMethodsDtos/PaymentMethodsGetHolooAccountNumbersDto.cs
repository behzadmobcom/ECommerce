using Ecommerce.Entities;

namespace Dto.PaymentMethodsDtos
{
    public class PaymentMethodsGetHolooAccountNumbersDto
    {
        public string C_Code { get; set; }

        public string Bank_Code { get; set; }

        public string Account_N { get; set; }

        public string? Branch_Name { get; set; }

        public string Key => Bank_Code + "-" + Account_N;

        public static implicit operator PaymentMethod(PaymentMethodsGetHolooAccountNumbersDto h)
        {
            return new PaymentMethod
            {
                AccountNumber = h.Account_N,
                BankCode = h.Bank_Code,
                BrunchName = h.Branch_Name
            };
        }
    }
}
