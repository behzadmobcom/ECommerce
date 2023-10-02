using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Entities
{
    public class Contact : BaseEntity
    {
        [Display(Name = "نام")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 50 کاراکتر")]
        [Required(ErrorMessage = @"{0} را وارد کنید")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "متن پیام")]
        [StringLength(5000, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 5000 کاراکتر")]
        [Required(ErrorMessage = @"{0} را وارد کنید")]
        public string Message { get; set; } = string.Empty;
       
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = @"{0} را وارد کنید")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 50 کاراکتر")]
        [EmailAddress(ErrorMessage = @"لطفا یک آدرس ایمیل صحیح وارد کنید")]
        public string Email { get; set; } = string.Empty;
        public DateTime CreateDateTime { get; set; } = DateTime.Now;

        [Display(Name = "موضوع")]
        [Required(ErrorMessage = @"{0} را وارد کنید")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 50 کاراکتر")]
        public string Subject { get; set; } = string.Empty;

        public string ReplayMessage { get; set; } = string.Empty;
    }
}
