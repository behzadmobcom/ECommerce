using Ecommerce.Entities;

namespace Dto.DiscountsDtos
{
    public class DiscountsGetLastDto
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

        //ForeignKey
        public ICollection<Price>? Prices { get; set; }
        public ICollection<Category>? Categories { get; set; }
        public ICollection<PurchaseOrder>? PurchaseOrders { get; set; }
        public ICollection<PurchaseOrderDetail>? PurchaseOrderDetails { get; set; }
    }
}
