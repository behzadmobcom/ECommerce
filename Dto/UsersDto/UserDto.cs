using Dto.BlogCommentsDtos;
using ECommerce.Dto.Base;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Entities;

public class UserDto :  BaseDto //, IdentityUser<int> 
{
     public DateTime RegisterDate { get; set; } 
     
    public bool IsActive { get; set; }  

    public bool IsColleague { get; set; } 

    public bool IsConfirmedColleague { get; set; }

    public string? Mobile { get; set; } 

    public bool IsHaveCustomerCode { get; set; }

    public string? CompanyName { get; set; }

    public string? NationalCode { get; set; }

    public string? FatherName { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public bool IsFeeder { get; set; }

    public string? PicturePath { get; set; }

    public DateTime Birthday { get; set; }

    public int? StateId { get; set; }

    public State? State { get; set; }

    public int? CityId { get; set; }

    public City? City { get; set; }

    public string? CustomerCode { get; set; }

    public string? CustomerCodeCustomer { get; set; }

    public int? UserRoleId { get; set; }
    public UserRole? UserRole { get; set; }

    public ICollection<Transaction>? Transactions { get; set; }

    public ICollection<PurchaseOrder>? PurchaseOrders { get; set; }

    public ICollection<BlogCommentDto>? BlogComments { get; set; }

    public int? HolooCompanyId { get; set; } = 1;

    public HolooCompany? HolooCompany { get; set; }
}