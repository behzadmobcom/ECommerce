namespace Dto.DiscountsDtos
{
    public class DiscountsGetWithTimeDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? ImagePath { get; set; }
        public string? Alt { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? Brand { get; set; }
        public DateTime? EndDateTime { get; set; }
    }
}
