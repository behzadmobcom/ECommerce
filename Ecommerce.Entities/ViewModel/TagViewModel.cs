namespace Ecommerce.Entities.ViewModel;

public class TagProductId
{
    public int Id { get; set; }
    public IEnumerable<int> ProductsId { get; set; }
}