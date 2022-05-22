using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;

namespace Bolouri.Areas.Admin.Pages.Suppliers;

public class CreateModel : PageModel
{
    private readonly ISupplierService _supplierService;

    public CreateModel(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [BindProperty] public Supplier Supplier { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var result = await _supplierService.Add(Supplier);
            if (result.Code == 0)
                return RedirectToPage("/Suppliers/Index",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        return Page();
    }
}