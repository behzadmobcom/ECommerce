using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.Colors
{
    public class DeleteModel : PageModel
    {
        private readonly IColorService _colorService;

        public DeleteModel(IColorService colorService)
        {
            _colorService = colorService;
        }

        public Color Color { get; set; }
        [TempData] public string Message { get; set; }

        [TempData] public string Code { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var result = await _colorService.GetById(id);
            if (result.Code == 0)
            {
                Color = result.ReturnData;
                return Page();
            }

            return RedirectToPage("/Colors/Index",
                new {area = "Admin", message = result.Message, code = result.Code.ToString()});
        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _colorService.Delete(id);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                    return RedirectToPage("/Colors/Index",
                new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
            }
            return Page();
        }
    }
}