using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.Admin.Pages.Departments;

public class DetailModel : PageModel
{
    private readonly IDepartmentService _departmentService;

    public DetailModel(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    public Department Department { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _departmentService.GetById(id);
        if (result.Code == 0)
        {
            Department = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/Departments/Index",
            new {message = result.Message, code = result.Code.ToString()});
    }
}