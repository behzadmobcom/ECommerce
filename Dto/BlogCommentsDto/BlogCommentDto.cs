﻿using ECommerce.Dto.Base;

namespace ECommerce.Dto.BlogCommentsDto;

public class BlogCommentDto : BaseDto
{
    public string? Text { get; set; }

    public string? Name { get; set; }

}
