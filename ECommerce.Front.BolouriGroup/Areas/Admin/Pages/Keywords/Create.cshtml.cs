using System.Threading.Tasks;
using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.Keywords
{
    public class CreateModel : PageModel
    {
        private readonly IKeywordService _keywordService;

        public CreateModel(IKeywordService keywordService)
        {
            _keywordService = keywordService;
        }

        [BindProperty] public Keyword Keyword { get; set; }

        [TempData] public string Message { get; set; }

        [TempData] public string Code { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await _keywordService.Add(Keyword);
                if (result.Code == 0)
                    return RedirectToPage("/Keywords/Index",
                        new {area = "Admin", message = result.Message, code = result.Code.ToString()});
                Message = result.Message;
                Code = result.Code.ToString();
                ModelState.AddModelError("", result.Message);
            }

            return Page();
        }
    }
}