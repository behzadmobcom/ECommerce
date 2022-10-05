using API.DataContext;
using API.Interface;
using API.Utilities;
using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class BlogRepository : AsyncRepository<Blog>, IBlogRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public BlogRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<Blog>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return PagedList<Blog>.ToPagedList(
            await _context.Blogs.Where(x => x.Title.Contains(paginationParameters.Search)).Include(x => x.Image)
                .Include(x=>x.Keywords).Include(x=>x.Tags).Include(x=>x.BlogAuthor).AsNoTracking()
                .OrderBy(on => on.Id).ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }

    public async Task<Blog> GetByTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.Blogs.Where(x => x.Title == title).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Blog> AddWithRelations(BlogViewModel blogViewModel, CancellationToken cancellationToken)
    {
        Blog blog = blogViewModel;
        blog.Keywords = new List<Keyword>();
        foreach (var id in blogViewModel.KeywordsId) blog.Keywords.Add(await _context.Keywords.FindAsync(id));
        blog.Tags = new List<Tag>();
        foreach (var id in blogViewModel.TagsId) blog.Tags.Add(await _context.Tags.FindAsync(id));
        blog.BlogCategory = new BlogCategory();
        blog.BlogCategory=(await _context.BlogCategories.FindAsync(blogViewModel.BlogCategoryId));
        blog.BlogAuthor = new BlogAuthor();
        blog.BlogAuthor = (await _context.BlogAuthors.FindAsync(blogViewModel.BlogAuthorId));

        await _context.Blogs.AddAsync(blog, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return blog;
    }

    public async Task<Blog> EditWithRelations(BlogViewModel blogViewModel, CancellationToken cancellationToken)
    {
        var blog = await _context.Blogs.Where(x => x.Id == blogViewModel.Id).Include(nameof(Blog.Tags))
            .Include(nameof(Blog.Keywords)).FirstAsync(cancellationToken);
        blog.Title = blogViewModel.Title;
        blog.Summary = blogViewModel.Summary;
        blog.Text = blogViewModel.Text;
        blog.CreateDateTime = blogViewModel.CreateDateTime;
        blog.EditDateTime = blogViewModel.EditDateTime;
        blog.PublishDateTime = blogViewModel.PublishDateTime;
        blog.Url = blogViewModel.Url;
        blog.BlogAuthorId = blogViewModel.BlogAuthorId;
        blog.BlogCategoryId = blogViewModel.BlogCategoryId;

        foreach (var blogKeyword in blog.Keywords) blog.Keywords.Remove(blogKeyword);
        foreach (var id in blogViewModel.KeywordsId) blog.Keywords.Add(await _context.Keywords.FindAsync(id));

        foreach (var tag in blog.Tags) blog.Tags.Remove(tag);
        foreach (var id in blogViewModel.TagsId) blog.Tags.Add(await _context.Tags.FindAsync(id));

        _context.Entry(blog).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return blog;
    }

    public async Task<IEnumerable<Blog>> GetWithInclude(int id, CancellationToken cancellationToken)
    {
        return await _context.Blogs.Where(x => x.BlogCategoryId == id).Include(nameof(Blog.BlogAuthor))
            .Include(nameof(Blog.Tags)).Include(nameof(Blog.Keywords)).ToListAsync(cancellationToken);
        ;
    }

    public async Task<Blog> GetByUrl(string url, CancellationToken cancellationToken)
    {
        return await _context.Blogs.Where(x => x.Url == url).FirstOrDefaultAsync(cancellationToken);
    }

    public IQueryable<Blog> GetBlogByIdWithInclude(int blogId)
    {
        //return _context.Blogs.AsNoTracking().Where(x => x.Id == blogId)
        //    .Include(c => c.Image)
        //    .Include(t => t.Tags)
        //    .Include(k => k.Keywords);

        var result = _context.Blogs.Where(x => x.Id == blogId).Include(nameof(Blog.BlogAuthor))
            .Include(nameof(Blog.Tags)).Include(nameof(Blog.Keywords)).Include(nameof(Blog.Image));

        return result;
    }
}