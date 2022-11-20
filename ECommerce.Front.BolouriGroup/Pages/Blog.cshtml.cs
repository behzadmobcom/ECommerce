using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
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
    public ServiceResult<List<Tag>> Tags { get; set; }

    public async Task OnGet(string blogCategoryId, string search, int pageNumber = 1, int pageSize = 3, int productSort = 1,
        string message = null, string code = null)
    {
        Blogs = await _blogService.TopBlogs(blogCategoryId, search, pageNumber, pageSize);
        Tags = await _tagService.GetAll();
    }
}