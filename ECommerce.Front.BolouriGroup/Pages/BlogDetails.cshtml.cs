using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Pages;

public class BlogDetailsModel : PageModel
{
    private readonly IBlogService _blogService;
    private readonly IBlogCategoryService _blogCategoryService;
    private readonly IBlogCommentService _blogCommentService;
    private readonly IUserService _userService;
    public BlogDetailsModel(IBlogService blogService , IBlogCategoryService blogCategoryService , 
        IBlogCommentService blogCommentService, IUserService userService)
    {
        _blogService = blogService;
        _blogCategoryService = blogCategoryService;
        _blogCommentService = blogCommentService;
        _userService = userService;
    }

    public BlogDetailsViewModel Blog {get; set;}
    public BlogCategory BlogCategory { get; set; }
    public ServiceResult<List<BlogViewModel>> Blogs { get; set; }
    public ServiceResult<List<BlogCategory>> Categories { get; set; }
    public ServiceResult<List<BlogComment>> BlogComments { get; set; }
    public BlogComment? BlogComment { get; set; }
    [BindProperty] public string? Message { get; set; }

    private async Task Initial(string blogUrl, int pageNumber = 1, int pageSize = 10)
    {
        var result = await _blogService.GetByUrl(blogUrl);
        if (result.Code > 0) return;
        Blog = result.ReturnData;
        var blogCategory = await _blogCategoryService.GetById(Blog.BlogCategoryId);
        BlogCategory = blogCategory.ReturnData;

        BlogComments = await _blogCommentService.GetAllAccesptedComments(search: System.Convert.ToString(Blog.Id),pageNumber,pageSize);

        Blogs = await _blogService.TopBlogs(null, null, 1, 3, 1);
        Categories = await _blogCategoryService.GetAll();
    }

        public async Task OnGet(string blogUrl, int pageNumber = 1, int pageSize = 10)
    {
        await Initial(blogUrl, pageNumber, pageSize);
    }

    public async Task OnPost(BlogComment blogComment, string blogUrl)
    {
        var user = await _userService.GetUser();
        if (user != null)
        {
            blogComment.UserId = user.ReturnData.Id;
            blogComment.Name = user.ReturnData.UserName;
        }

        var resultProduct = await _blogService.GetByUrl(blogUrl);
        if (resultProduct.Code > 0) return;
        Blog = resultProduct.ReturnData;
        blogComment.BlogId =Blog.Id;

        if (ModelState.IsValid)
        {
            var result = await _blogCommentService.Add(blogComment);
            Message = result.Message;
        }
        else
        {
            Message = "نظر شما ثبت نگردید.";
        }
        await Initial(blogUrl);
    }
}