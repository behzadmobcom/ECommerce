using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Pages;

public class BlogDetailsModel : PageModel
{
    private readonly IBlogService _blogService;
    public BlogDetailsModel(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public BlogDetailsViewModel Blog {get; set;}

    public async Task OnGet(string blogUrl)
    {
        var result=await _blogService.GetByUrl("saman");
        if (result.Code > 0) return;
        Blog = result.ReturnData;        
    }
}