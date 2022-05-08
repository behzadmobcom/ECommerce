using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.Colors
{
    public class DetailModel : PageModel
    {
        private readonly IColorService _colorService;

        public DetailModel(IColorService colorService)
        {
            _colorService = colorService;
        }

        public Color Color { get; set; }

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
    }
}