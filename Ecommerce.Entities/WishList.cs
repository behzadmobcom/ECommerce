using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ecommerce.Entities;

public class WishList : BaseEntity
{
    [Required] public int UserId { get; set; }

    [JsonIgnore] public User? User { get; set; }

    public int PriceId { get; set; }

    [JsonIgnore] public Price? Price { get; set; }
}