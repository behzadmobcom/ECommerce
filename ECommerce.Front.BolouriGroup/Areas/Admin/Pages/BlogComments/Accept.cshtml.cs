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
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public async Task OnGet(int id, string message = null, string code = null)
    {
        Message = message;
        Code = code;
        var BlogCommentResult = await _blogCommentService.GetById(id);
        BlogComment = BlogCommentResult.ReturnData;
    }
}