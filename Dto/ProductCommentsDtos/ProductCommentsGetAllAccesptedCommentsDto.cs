﻿using Ecommerce.Entities;

namespace Dto.ProductCommentsDtos
{
    public class ProductCommentsGetAllAccesptedCommentsDto
    {
        public string Text { get; set; }

        public bool IsAccepted { get; set; }


        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateTime { get; set; }

        public bool IsRead { get; set; }

        public bool IsAnswered { get; set; }


        public string? Email { get; set; }


        public string? Name { get; set; }

        //ForeignKey
        public int? UserId { get; set; }
        public User? User { get; set; }

        public int? AnswerId { get; set; }
        public ProductComment? Answer { get; set; }

        public int? ProductId { get; set; }
        public Product? Product { get; set; }

        public int? EmployeeId { get; set; }

        public Employee? Employee { get; set; }
    }
}
