using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Entities;

public abstract class RootEntity : BaseEntity
{
    public int? CreatorUserId { get; set; }
    public int? EditorUserId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
}
