using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;

namespace Bolouri.Areas.Admin.Pages.Departments;

public class DeleteModel : PageModel
{
    private readonly IDepartmentService _departmentService;

    public DeleteModel(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    public Department Department { get; set; }
    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _departmentService.GetById(id);
        if (result.Code == 0)
        {
            Department = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/Departments/Index",
            new {area = "Admin", message = result.Message, code = result.Code.ToString()});
    }

    public async Task<IActionResult> OnPost(int id)
    {
        if (ModelState.IsValid)
        {
            var result = await _departmentService.Delete(id);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                return RedirectToPage("/Departments/Index",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
        }

        return Page();
    }
}