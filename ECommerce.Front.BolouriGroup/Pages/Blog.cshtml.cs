using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Pages;

public class BlogModel : PageModel
{
    private readonly IBlogService _blogService;
    private readonly IBlogCategoryService _blogCategoryService;
    private readonly ITagService _tagService;

    public BlogModel(IBlogService blogService, IBlogCategoryService blogCategoryService, ITagService tagService)
    {
        _blogService = blogService;
        _blogCategoryService = blogCategoryService;
        _tagService = tagService;
    }

    public ServiceResult<List<BlogViewModel>> Blogs { get; set; }
    [BindProperty] public ServiceResult<List<Tag>> Tags { get; set; }
    public string? Search { get; set; }

    public async Task OnGet(string blogCategoryId, string search, int pageNumber = 1, int pageSize = 3, int productSort = 1,
        string message = null, string code = null)
    {
        if (int.TryParse(blogCategoryId, out var intResult))
        {
            Blogs = await _blogService.TopBlogs(blogCategoryId, search, pageNumber, pageSize);
        }
        else
        {
            Blogs = await _blogService.TopBlogsByTagText(null, blogCategoryId, pageNumber, pageSize);
        }

        Tags = await _tagService.GetAllBlogTags();
    }

    public async Task OnPost(string blogCategoryId, string search)
    {
        Blogs = await _blogService.TopBlogs(blogCategoryId, search, 1, 3);
        Tags = await _tagService.GetAllBlogTags();

    }
}