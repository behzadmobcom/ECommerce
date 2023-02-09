using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.MessagesDtos
{
    public class MessagesGetByIdDto
    {
        public string Name { get; set; }

        public string? Email { get; set; }

        public string Body { get; set; }

        public int? UserId { get; set; }
        public DateTime DateTime { get; set; }
        public virtual UserDto User { get; set; }
    }
}
