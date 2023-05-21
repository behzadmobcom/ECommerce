using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.Admin.Pages.Suppliers;

public class DetailModel : PageModel
{
    private readonly ISupplierService _supplierService;

    public DetailModel(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    public Supplier Supplier { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _supplierService.GetById(id);
        if (result.Code == 0)
        {
            Supplier = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/Suppliers/Index",
            new { message = result.Message, code = result.Code.ToString()});
    }
}