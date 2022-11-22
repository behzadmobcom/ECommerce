using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Pages;

public class BlogDetailsModel : PageModel
{
    private readonly IBlogService _blogService;
    private readonly IBlogCategoryService _blogCategoryService;
    public BlogDetailsModel(IBlogService blogService , IBlogCategoryService blogCategoryService)
    {
        _blogService = blogService;
        _blogCategoryService = blogCategoryService;
    }

    public BlogDetailsViewModel Blog {get; set;}
    public BlogCategory BlogCategory { get; set; }
    public ServiceResult<List<BlogViewModel>> Blogs { get; set; }
    public ServiceResult<List<BlogCategory>> Categories { get; set; }


    public async Task OnGet(string blogUrl)
    {
        var result=await _blogService.GetByUrl(blogUrl);
        if (result.Code > 0) return;
        Blog = result.ReturnData;
        var blogCategory = await _blogCategoryService.GetById(Blog.BlogCategoryId);
        BlogCategory = blogCategory.ReturnData;

        Blogs = await _blogService.TopBlogs(null, null, 1, 3, 1);
        Categories = await _blogCategoryService.GetAll();
    }
}