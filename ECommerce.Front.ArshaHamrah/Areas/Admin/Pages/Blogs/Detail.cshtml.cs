using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.ArshaHamrah.Areas.Admin.Pages.Blogs;

[Authorize(Roles = "Admin,SuperAdmin")]
public class DetailModel : PageModel
{
    private readonly IBlogService _blogService;

    public DetailModel(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public Blog Blog { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _blogService.GetById(id);
        if (result.Code == 0)
        {
            Blog = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/Blogs/Index",
            new {area = "Admin", message = result.Message, code = result.Code.ToString()});
    }
}