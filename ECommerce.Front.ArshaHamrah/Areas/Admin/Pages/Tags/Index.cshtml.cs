using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.Tags
{
    public class IndexModel : PageModel
    {
        private readonly ITagService _tagService;

        public IndexModel(ITagService tagService)
        {
            _tagService = tagService;
        }

        public ServiceResult<List<Tag>> Tags { get; set; }

        [TempData] public string Message { get; set; }

        [TempData] public string Code { get; set; }

        public async Task<IActionResult> OnGet(string search = "", int pageNumber = 1, int pageSize = 10, string message = null, string code = null)
        {
            Message = message;
            Code = code;
            var result = await _tagService.Load(search, pageNumber, pageSize);
            if (result.Code == ServiceCode.Success)
            {
                Message = result.Message;
                Code = result.Code.ToString();
                Tags = result;
                return Page();
            }

            return RedirectToPage("/index", new { message = result.Message, code = result.Code.ToString() });
        }
    }
}