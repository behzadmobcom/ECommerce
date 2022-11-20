using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ecommerce.Entities;

public class ProductUserRank : BaseEntity
{
    [Display(Name = "امتیاز")] public int Stars { get; set; }


    [Required] public int UserId { get; set; }

    [JsonIgnore] public User? User { get; set; }

    public int ProductId { get; set; }

    [JsonIgnore] public Product? Product { get; set; }
}