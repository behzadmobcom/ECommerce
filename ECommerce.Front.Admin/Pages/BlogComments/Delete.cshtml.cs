using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.Admin.Pages.BlogComments;
 
    public class DeleteModel : PageModel
    {

    private readonly IBlogService _blogService;
    private readonly IBlogCommentService _blogCommentService;
    public DeleteModel(IBlogCommentService blogCommentService, IBlogService blogService)
    {
        _blogCommentService = blogCommentService;
        _blogService = blogService;

    }
    [BindProperty] public BlogComment BlogComment { get; set; }
    public Blog Blog { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public async Task OnGet(int id)
    {
        var BlogCommentResult = await _blogCommentService.GetById(id);
        BlogComment = BlogCommentResult.ReturnData;
        int _blogId = BlogComment.BlogId ?? default(int);
        var ProductResult = await _blogService.GetById(_blogId);
        Blog = ProductResult.ReturnData;
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            if (BlogComment.AnswerId != null) await _blogCommentService.Delete(BlogComment.AnswerId ?? default(int));
            var result = await _blogCommentService.Delete(BlogComment.Id);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                return RedirectToPage("/BlogComments/Index",
                    new {message = result.Message, code = result.Code.ToString() });
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }
        return Page();
    }


}
 
