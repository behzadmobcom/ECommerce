using ECommerce.Services.IServices;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bolouri.Pages;

public class BlogModel : PageModel
{
    private readonly IBlogService _blogService;
    private readonly IBlogCategoryService _blogCategoryService;

    public BlogModel(IBlogService blogService, IBlogCategoryService blogCategoryService)
    {
        _blogService = blogService;
        _blogCategoryService = blogCategoryService;
    }

    public ServiceResult<List<BlogViewModel>> Blogs { get; set; }

    public async Task OnGet(string path, string search, int pageNumber = 1, int pageSize = 20, int productSort = 1,
        string message = null, string code = null)
    {
        Blogs = await _blogService.TopBlogs(path, search, pageNumber, pageSize);
    }
}