using Entities;

namespace ECommerce.Application.Responses.Products;

public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Price> List { get; set; }
    public int Stars { get; set; }
    public string ImagePath { get; set; }
    public string Alt { get; set; }
    public string? Description { get; set; }
    public string Url { get; set; }
    public string Brand { get; set; }
}