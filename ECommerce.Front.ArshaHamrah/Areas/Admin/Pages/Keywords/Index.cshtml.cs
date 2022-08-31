using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ArshaHamrah.Areas.Admin.Pages.Keywords;

[Authorize(Roles = "Admin,SuperAdmin")]
public class IndexModel : PageModel
{
    private readonly IKeywordService _keywordService;

    public IndexModel(IKeywordService keywordService)
    {
        _keywordService = keywordService;
    }

    public ServiceResult<List<Keyword>> Keywords { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task<IActionResult> OnGet(string search = "", int pageNumber = 1, int pageSize = 10,
        string message = null, string code = null)
    {
        Message = message;
        Code = code;
        var result = await _keywordService.Load(search, pageNumber, pageSize);

        if (result.Code == ServiceCode.Success)
        {
            result.PaginationDetails.Address = "/Keywords/Index";
            Message = result.Message;
            Code = result.Code.ToString();
            Keywords = result;
            return Page();
        }

        return RedirectToPage("/index", new {message = result.Message, code = result.Code.ToString()});
    }
}