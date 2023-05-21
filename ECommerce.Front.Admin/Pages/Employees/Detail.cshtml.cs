using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.Admin.Pages.Employees;

public class DetailModel : PageModel
{
    private readonly IEmployeeService _employeeService;

    public DetailModel(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public Employee Employee { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _employeeService.GetById(id);
        if (result.Code == 0)
        {
            Employee = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/Employees/Index",
            new {message = result.Message, code = result.Code.ToString()});
    }
}