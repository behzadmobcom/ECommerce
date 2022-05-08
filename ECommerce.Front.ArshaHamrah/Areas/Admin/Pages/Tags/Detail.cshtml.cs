using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.Tags
{
    public class DetailModel : PageModel
    {
        private readonly ITagService _tagService;

        public DetailModel(ITagService tagService)
        {
            _tagService = tagService;
        }

        public Tag Tag { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var result = await _tagService.GetById(id);
            if (result.Code == 0)
            {
                Tag = result.ReturnData;
                return Page();
            }

            return RedirectToPage("/Tags/Index",
                new {area = "Admin", message = result.Message, code = result.Code.ToString()});
        }
    }
}