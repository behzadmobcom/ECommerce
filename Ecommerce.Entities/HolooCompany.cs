using System.ComponentModel.DataAnnotations;

namespace Entities;

public class HolooCompany : BaseEntity
{
    [Display(Name = "نام شرکت")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = @"حداقل 83 و حداکثر 50 کاراکتر")]
    public string Name { get; set; }

    [Display(Name = "رشته اتصال")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string ConnectionString { get; set; }

    ////ForeignKey
    //[JsonIgnore]
    //public ICollection<Product> Products { get; set; }

    //[JsonIgnore]
    //public ICollection<User> Users { get; set; }

    //[JsonIgnore]
    //public ICollection<Transaction> Transactions { get; set; }

    //[JsonIgnore]
    //public ICollection<Unit> Units { get; set; }

    //[JsonIgnore]
    //public ICollection<Employee> Employees { get; set; }
}