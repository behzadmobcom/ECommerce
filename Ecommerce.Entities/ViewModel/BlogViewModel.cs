using System;
using System.Collections.Generic;

namespace Entities.ViewModel
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        
        public string Summary { get; set; }

        public DateTime CreateDateTime { get; set; }= DateTime.Now;

        public DateTime EditDateTime { get; set; } = DateTime.Now;

        public DateTime PublishDateTime { get; set; } = DateTime.Now;

        public string Url { get; set; }

        public int Like { get; set; }

        public int Dislike { get; set; }

        public int Visit { get; set; }

        //ForeignKey
        public int BlogAuthorId { get; set; }

        public int BlogCategoryId { get; set; }

        public ICollection<int> KeywordsId { get; set; }

        public ICollection<int> TagsId { get; set; }

        public static implicit operator BlogViewModel(Blog x)
        {
            return new BlogViewModel
            {
                Title = x.Title,
                Summary = x.Summary,
                Text = x.Text,
                CreateDateTime = x.CreateDateTime,
                EditDateTime = x.EditDateTime,
                PublishDateTime = x.PublishDateTime,
                Url=x.Url,
                BlogAuthorId = x.BlogAuthorId,
                BlogCategoryId = x.BlogCategoryId
            };
        }

        public static implicit operator Blog(BlogViewModel x)
        {
            return new Blog
            {
                Title = x.Title,
                Summary = x.Summary,
                Text = x.Text,
                CreateDateTime = x.CreateDateTime,
                EditDateTime = x.EditDateTime,
                PublishDateTime = x.PublishDateTime,
                Url = x.Url,
                BlogAuthorId = x.BlogAuthorId,
                BlogCategoryId = x.BlogCategoryId
            };
        }
    }
}
