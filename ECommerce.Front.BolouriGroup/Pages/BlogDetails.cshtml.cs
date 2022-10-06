using ECommerce.Services.IServices;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bolouri.Pages;

public class BlogDetailsModel : PageModel
{
    private readonly IBlogService _blogService;
    public BlogDetailsModel(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public BlogDetailsViewModel Blog {get; set;}

    public async void OnGet(string blogUrl)
    {
        var result=await _blogService.GetByUrl("saman");
        if (result.Code > 0) return;
        Blog = result.ReturnData;        
    }
}