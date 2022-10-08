using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;

namespace Bolouri.Areas.Admin.Pages.Employees;

public class IndexModel : PageModel
{
    private readonly IEmployeeService _employeeService;

    public IndexModel(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public ServiceResult<List<Employee>> Employees { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task<IActionResult> OnGet(string search = "", int pageNumber = 1, int pageSize = 10,
        string message = null, string code = null)
    {
        Message = message;
        Code = code;
        var result = await _employeeService.GetAll(search, pageNumber, pageSize);
        if (result.Code == ServiceCode.Success)
        {
            Message = result.Message;
            Code = result.Code.ToString();
            Employees = result;
            return Page();
        }

        return RedirectToPage("/index", new {message = result.Message, code = result.Code.ToString()});
    }
}