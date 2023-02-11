using Ecommerce.Entities.ViewModel;
using Ecommerce.Entities;

namespace Dto.BlogsDtos
{
    public class BlogsPostDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }

        public string Summary { get; set; }

        public DateTime CreateDateTime { get; set; } = DateTime.Now;

        public DateTime EditDateTime { get; set; } = DateTime.Now;

        public DateTime PublishDateTime { get; set; } = DateTime.Now;

        public string Url { get; set; }

        public int Like { get; set; }

        public int Dislike { get; set; }

        public int Visit { get; set; }

        public int CommentCount { get; set; }

        //ForeignKey
        public int BlogAuthorId { get; set; }
        public BlogAuthor? BlogAuthor { get; set; }

        public int BlogCategoryId { get; set; }

        //public ICollection<int> KeywordsId { get; set; }

        //public ICollection<int> TagsId { get; set; }
        public List<int>? KeywordsId { get; set; } = new List<int>();
        public List<Keyword>? Keywords { get; set; }
        public List<int>? TagsId { get; set; } = new List<int>();
        public List<Tag>? Tags { get; set; }
        public Image? Image { get; set; }

        public static implicit operator BlogViewModel(Blogs x)
        {
            return new BlogViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Summary = x.Summary,
                Text = x.Text,
                CreateDateTime = x.CreateDateTime,
                EditDateTime = x.EditDateTime,
                PublishDateTime = x.PublishDateTime,
                Url = x.Url,
                BlogAuthorId = x.BlogAuthorId,
                BlogCategoryId = x.BlogCategoryId,
                TagsId = x.Tags.Select(x => x.Id).ToList(),
                KeywordsId = x.Keywords.Select(x => x.Id).ToList(),
                Tags = x.Tags.ToList(),
                Keywords = x.Keywords.ToList(),
                Image = x.Image,
                BlogAuthor = x.BlogAuthor,
                CommentCount = x.BlogComments.Count
            };
        }

        public static implicit operator Blog(BlogViewModel x)
        {
            return new Blog
            {
                Id = x.Id,
                Title = x.Title,
                Summary = x.Summary,
                Text = x.Text,
                CreateDateTime = x.CreateDateTime,
                EditDateTime = x.EditDateTime,
                PublishDateTime = x.PublishDateTime,
                Url = x.Url,
                BlogAuthorId = x.BlogAuthorId,
                BlogCategoryId = x.BlogCategoryId,
                Tags = x.Tags?.ToList(),
                Keywords = x.Keywords?.ToList(),
                Image = x.Image,
                BlogAuthor = x.BlogAuthor
            };
        }
    }



    public class BlogDetailsViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }

        public string Summary { get; set; }

        public DateTime CreateDateTime { get; set; } = DateTime.Now;

        public DateTime EditDateTime { get; set; } = DateTime.Now;

        public DateTime PublishDateTime { get; set; } = DateTime.Now;

        public string Url { get; set; }

        public int Like { get; set; }

        public int Dislike { get; set; }

        public int Visit { get; set; }

        //ForeignKey
        public int BlogAuthorId { get; set; }
        public BlogAuthor? BlogAuthor { get; set; }

        public int BlogCategoryId { get; set; }

        //public ICollection<int> KeywordsId { get; set; }

        //public ICollection<int> TagsId { get; set; }
        public List<int>? KeywordsId { get; set; } = new List<int>();
        public List<Keyword>? Keywords { get; set; }
        public List<int>? TagsId { get; set; } = new List<int>();
        public List<Tag>? Tags { get; set; }
        public Image? Image { get; set; }
        public List<int>? BlogCommentsId { get; set; } = new List<int>();
        public List<BlogComment>? BlogComments { get; set; }

        public static implicit operator BlogDetailsViewModel(Blog x)
        {
            return new BlogDetailsViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Summary = x.Summary,
                Text = x.Text,
                CreateDateTime = x.CreateDateTime,
                EditDateTime = x.EditDateTime,
                PublishDateTime = x.PublishDateTime,
                Url = x.Url,
                BlogAuthorId = x.BlogAuthorId,
                BlogCategoryId = x.BlogCategoryId,
                TagsId = x.Tags.Select(x => x.Id).ToList(),
                KeywordsId = x.Keywords.Select(x => x.Id).ToList(),
                Tags = x.Tags.ToList(),
                Keywords = x.Keywords.ToList(),
                Image = x.Image,
                BlogAuthor = x.BlogAuthor,
                BlogComments = x.BlogComments.ToList(),
                BlogCommentsId = x.BlogComments.Select(x => x.Id).ToList()
            };
        }

        public static implicit operator Blog(BlogDetailsViewModel x)
        {
            return new Blog
            {
                Id = x.Id,
                Title = x.Title,
                Summary = x.Summary,
                Text = x.Text,
                CreateDateTime = x.CreateDateTime,
                EditDateTime = x.EditDateTime,
                PublishDateTime = x.PublishDateTime,
                Url = x.Url,
                BlogAuthorId = x.BlogAuthorId,
                BlogCategoryId = x.BlogCategoryId,
                Tags = x.Tags?.ToList(),
                Keywords = x.Keywords?.ToList(),
                Image = x.Image,
                BlogAuthor = x.BlogAuthor,
                BlogComments = x.BlogComments.ToList()
            };
        }
    }
}
