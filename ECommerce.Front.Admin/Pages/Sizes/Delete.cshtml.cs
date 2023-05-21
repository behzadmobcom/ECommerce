using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.Sizes;

public class DeleteModel : PageModel
{
    private readonly ISizeService _sizeService;

    public DeleteModel(ISizeService sizeService)
    {
        _sizeService = sizeService;
    }

    public Size Size { get; set; }
    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _sizeService.GetById(id);
        if (result.Code == 0)
        {
            Size = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/Sizes/Index",
            new {area = "Admin", message = result.Message, code = result.Code.ToString()});
    }

    public async Task<IActionResult> OnPost(int id)
    {
        if (ModelState.IsValid)
        {
            var result = await _sizeService.Delete(id);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                return RedirectToPage("/Sizes/Index",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
        }

        return Page();
    }
}