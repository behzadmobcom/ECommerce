using Ecommerce.Entities.Helper;
using Ecommerce.Entities;

namespace Dto.PricesDtos
{
    public class PricesGetByIdDto
    {
        public enum HolooSellNumber
        {
            خالی = 0,
            Sel_Price = 1,
            Sel_Price2 = 2,
            Sel_Price3 = 3,
            Sel_Price4 = 4,
            Sel_Price5 = 5,
            Sel_Price6 = 6,
            Sel_Price7 = 7,
            Sel_Price8 = 8,
            Sel_Price9 = 9,
            Sel_Price10 = 10
        }

        public decimal Amount { get; set; }

        public int MinQuantity { get; set; } = 1;

        public int MaxQuantity { get; set; }

        public bool IsColleague { get; set; } = false;

        public bool IsDefault { get; set; } = false;

        public HolooSellNumber? SellNumber { get; set; }

        public string? ArticleCode { get; set; }

        public string? ArticleCodeCustomer { get; set; }

        public double Exist { get; set; } = 0;

        public Grade Grade { get; set; }
        //ForeignKey

        public int ProductId { get; set; }

        //[JsonIgnore]
        public Product? Product { get; set; }

        public int? UnitId { get; set; }
        public Unit? Unit { get; set; }

        public int? SizeId { get; set; }
        public Size? Size { get; set; }

        public int? ColorId { get; set; } = 1;
        public Color? Color { get; set; }

        public int? CurrencyId { get; set; }
        public Currency? Currency { get; set; }

        public int? DiscountId { get; set; }
        public Discount? Discount { get; set; }
    }
}
