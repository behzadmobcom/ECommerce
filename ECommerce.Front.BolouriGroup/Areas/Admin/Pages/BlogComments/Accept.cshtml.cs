using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.BlogComments;

public class AcceptModel : PageModel
{
    private readonly IBlogCommentService _blogCommentService;
    private readonly IBlogService _blogService;
    public AcceptModel(IBlogCommentService blogCommentService, IBlogService blogService)
    {
        _blogCommentService = blogCommentService;
        _blogService = blogService;

    }

    [BindProperty] public BlogComment BlogComment { get; set; }
    [TempData] public string? Message { get; set; }
    [TempData] public string Code { get; set; }
    public Blog Blog { get; set; }

    public async Task OnGet(int id, string message = null, string code = null)
    {
        Message = message;
        Code = code;
        var BlogCommentResult = await _blogCommentService.GetById(id);
        BlogComment = BlogCommentResult.ReturnData;
        int _blogId = BlogComment.BlogId ?? default(int);
        var ProductResult = await _blogService.GetById(_blogId);
        Blog = ProductResult.ReturnData;
    }
    public async Task<IActionResult> OnPost()
    {
        try
        {
            if (BlogComment.Answer!.Text == null && BlogComment.AnswerId == null) BlogComment.Answer = null;
            var result = await _blogCommentService.Accept(BlogComment);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                return RedirectToPage("/BlogComments/Index",
                    new { area = "Admin", message = result.Message, code = result.Code.ToString() });
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
            return RedirectToPage("/BlogComments/Accept",
                        new { id = BlogComment.Id, area = "Admin", message = $"پیغام خطا:{Message}", code = Code });
        }
        catch
        {
            return RedirectToPage("/BlogComments/Accept",
                        new { id = BlogComment.Id, area = "Admin", message = "پیغام خطای غیر منتظره", code = "Error" });
        }
    }
}