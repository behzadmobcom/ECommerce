using Ecommerce.Entities;

namespace Dto.MessagesDtos
{
    public class MessagesPostDto
    {
        public string Name { get; set; }

        public string? Email { get; set; }

        public string Body { get; set; }

        public int? UserId { get; set; }
        public DateTime DateTime { get; set; }
        public virtual User User { get; set; }
    }
}
