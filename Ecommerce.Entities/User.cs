using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class User : IdentityUser<int>, IBaseEntity<int>
    {
        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; } = DateTime.Now;

        [Display(Name = "فعال بودن حساب کاربری")]
        public bool IsActive { get; set; } = false;

        [Display(Name = "همکار", Description = "در صورتی که همکار هستید باید مدارک مورد نیاز را ارسال کنید تا تایید شود")]
        public bool IsColleague { get; set; } = false;

        [Display(Name = "همکار تایید شده")]
        public bool IsConfirmedColleague { get; set; } = false;

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "حتما باید 11 کاراکتر باشد")]
        public string Mobile { get; set; } = "No Mobile";

        [Display(Name = "داشتن کد مشتری در فاکتور های قبلی")]
        public bool IsHaveCustomerCode { get; set; } = false;

        //UserColleague
        [Display(Name = "تصویر مدرک")]
        public string? LicensePath { get; set; }

        [Display(Name = "نام شرکت")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "حداقل 3 و حداکثر 50 کاراکتر")]
        public string? CompanyName { get; set; }

        //UserExtraInfo
        [Display(Name = "کدملی")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "تعداد ارقام غلط است")]
        public string? NationalCode { get; set; }

        [Display(Name = "نام پدر")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "حداقل 3 و حداکثر 30 کاراکتر")]
        public string? FatherName { get; set; }

        //UserInformation
        [Display(Name = "نام")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = @"حداکثر 30 کاراکتر")]
        public string? FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [StringLength(50, ErrorMessage = @"حداکثر 50 کاراکتر")]
        public string? LastName { get; set; }

        [Display(Name = "خبرنامه")]
        public bool IsFeeder { get; set; }

        [Display(Name = "عکس")]
        public string? PicturePath { get; set; }

        [Display(Name = "تاریخ تولد")]
        public DateTime Birthday { get; set; }

        public int? StateId { get; set; }
        [JsonIgnore]
        public State? State { get; set; }

        public int? CityId { get; set; }
        [JsonIgnore]
        public City? City { get; set; }

        //ForeignKey
        [StringLength(5, MinimumLength = 5, ErrorMessage = "حتما باید 5 کاراکتر باشد")]
        public string? CustomerCode { get; set; }

        [StringLength(12)]
        public string? CustomerCodeCustomer { get; set; }

        public int? UserRoleId { get; set; }
        public UserRole? UserRole { get; set; }

        public ICollection<Transaction>? Transactions { get; set; }

        public ICollection<PurchaseOrder>? PurchaseOrders { get; set; }

        public ICollection<BlogComment>? BlogComments { get; set; }

        public int? HolooCompanyId { get; set; } = 1;

        public HolooCompany? HolooCompany { get; set; }
    }
}
