using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.Suppliers
{
    public class DeleteModel : PageModel
    {
        private readonly ISupplierService _supplierService;

        public DeleteModel(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public Supplier Supplier { get; set; }
        [TempData] public string Message { get; set; }

        [TempData] public string Code { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var result = await _supplierService.GetById(id);
            if (result.Code == 0)
            {
                Supplier = result.ReturnData;
                return Page();
            }

            return RedirectToPage("/Suppliers/Index",
                new {area = "Admin", message = result.Message, code = result.Code.ToString()});
        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _supplierService.Delete(id);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                    return RedirectToPage("/Suppliers/Index",
                new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
            }
            return Page();
        }
    }
}