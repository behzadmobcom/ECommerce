using System.Threading.Tasks;
using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.Tags
{
    public class EditModel : PageModel
    {
        private readonly ITagService _tagService;

        public EditModel(ITagService tagService)
        {
            _tagService = tagService;
        }
        [BindProperty] public Tag Tag { get; set; }
        [TempData] public string Message { get; set; }
        [TempData] public string Code { get; set; }

        public async Task OnGet(int id)
        {
            var result = await _tagService.GetById(id);
            Tag = result.ReturnData;
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await _tagService.Edit(Tag);
                Message = result.Message;
                Code = result.Code.ToString();
                if (result.Code == 0)
                    return RedirectToPage("/Tags/Index",
                        new {area = "Admin", message = result.Message, code = result.Code.ToString()});
                Message = result.Message;
                Code = result.Code.ToString();
                ModelState.AddModelError("", result.Message);
            }

            return Page();
        }
    }
}