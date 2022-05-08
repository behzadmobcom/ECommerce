using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities
{
    public class SendInformation : BaseEntity
    {
        [Display(Name = "نام تحویل گیرنده")]
        [Required(ErrorMessage = @"{0} را وارد کنید")]
        public string RecipientName { get; set; }

        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = @"{0} را وارد کنید")]
        public string Mobile { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = @"{0} را وارد کنید")]
        public string Address { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "حتما باید 10 کاراکتر باشد")]
        public string? PostalCode { get; set; }

        //ForeignKey
        public int? UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

        [Required(ErrorMessage = @"{0} را وارد کنید")]
        public int StateId { get; set; }
        public State State { get; set; }

        [Required(ErrorMessage = @"{0} را وارد کنید")]
        public int CityId { get; set; }
        public City City { get; set; }
    }
}
