namespace Entities.ViewModel
{
    public class PurchaseOrderViewModel
    {
        public int ProductId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string ImagePath { get; set; }
        public string Alt { get; set; }
        public int Price { get; set; }
        public int SumPrice { get; set; }
        public int Quantity { get; set; }
        public double Exist { get; set; }

        public bool IsColleague { get; set; }

        //ForeignKey
        public int UserId { get; set; }
    }
}
