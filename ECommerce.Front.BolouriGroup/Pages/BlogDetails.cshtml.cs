using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Front.BolouriGroup.Models;
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
    private readonly ITagService _tagService;
    public BlogDetailsModel(IBlogService blogService, IBlogCategoryService blogCategoryService,
        IBlogCommentService blogCommentService, IUserService userService, ITagService tagService)
    {
        _blogService = blogService;
        _blogCategoryService = blogCategoryService;
        _blogCommentService = blogCommentService;
        _userService = userService;
        _tagService = tagService;
    }

    public BlogDetailsViewModel Blog { get; set; }
    public BlogCategory BlogCategory { get; set; }
    public ServiceResult<List<BlogViewModel>> Blogs { get; set; }
    public ServiceResult<List<BlogCategory>> Categories { get; set; }
    public ServiceResult<List<BlogComment>> BlogComments { get; set; }
    public BlogComment? BlogComment { get; set; }
    [BindProperty] public string? Message { get; set; }
    [BindProperty] public ServiceResult<List<Tag>> Tags { get; set; }

    private async Task Initial(string blogUrl, int pageNumber = 1, int pageSize = 10)
    {
        var result = await _blogService.GetByUrl(blogUrl);
        if (result.Code > 0) return;
        Blog = result.ReturnData;
        var blogCategory = await _blogCategoryService.GetById(Blog.BlogCategoryId);
        BlogCategory = blogCategory.ReturnData;

        BlogComments = await _blogCommentService.GetAllAccesptedComments(search: Convert.ToString(Blog.Id), pageNumber, pageSize);

        Blogs = await _blogService.TopBlogs(null, null, 1, 3, 1);
        Categories = await _blogCategoryService.GetAll();

        Tags = await _tagService.GetAllBlogTags();
    }

    public async Task OnGet(string blogUrl, int pageNumber = 1, int pageSize = 10)
    {
        await Initial(blogUrl, pageNumber, pageSize);
    }

    public async Task<IActionResult> OnGetComment(string blogUrl, string name, string email, string text)
    {
        
        VerifyResultData resultData = new();
        if (string.IsNullOrEmpty(name))
        {
            resultData.Description = "لطفا نام خود را برای ثبت نظر وارد کنید";
            resultData.Succeed = false;
            return new JsonResult(resultData);
        }
        if (string.IsNullOrEmpty(email))
        {
            resultData.Description = "لطفا ایمیل خود را برای ثبت نظر وارد کنید";
            resultData.Succeed = false;
            return new JsonResult(resultData);
        }
        if (string.IsNullOrEmpty(text))
        {
            resultData.Description = "لطفا نظر خود را برای ثبت نظر وارد کنید";
            resultData.Succeed = false;
            return new JsonResult(resultData);
        }
        BlogComment blogComment = new()
        {
            Email = email,
            Name = name,
            Text = text,
            User = null,
            UserId = null
        };

        var user = await _userService.GetUser();
        if (user.Code == 0)
        {
            blogComment.UserId = user.ReturnData.Id;
        }

        var resultProduct = await _blogService.GetByUrl(blogUrl);
        if (resultProduct.Code > 0) return new JsonResult(resultData);

        Blog = resultProduct.ReturnData;
        blogComment.BlogId = Blog.Id;

        var result = await _blogCommentService.Add(blogComment);
        if (result.Code == 0)
        {
            resultData.Description = "نظر شما ثبت شد، پس از تایید توسط ادمین سایت، نمایش داده می شود";
            resultData.Succeed = true;
        }
        else
        {
            resultData.Description = "ثبت نظر با مشکل مواجه شد. لطفا مجددا تست کنید";
            resultData.Succeed = false;
        }
        return new JsonResult(resultData);
    }
}