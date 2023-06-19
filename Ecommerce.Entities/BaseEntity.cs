using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Entities;

public abstract class BaseEntity<TKey> : IBaseEntity<TKey>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public TKey? Id { get; set; }
}

public abstract class BaseEntity : BaseEntity<int>
{
}