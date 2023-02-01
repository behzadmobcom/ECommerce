using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dto.MessagesDtos
{
    public class MessagesGetDto
    {
        public string Name { get; set; }

        public string? Email { get; set; }

        public string Body { get; set; }

        public int? UserId { get; set; }
        public DateTime DateTime { get; set; }
        public virtual User User { get; set; }
    }
}
