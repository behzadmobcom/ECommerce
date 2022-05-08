using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.Departments
{
    public class IndexModel : PageModel
    {
        private readonly IDepartmentService _departmentService;

        public IndexModel(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public List<Department> Departments { get; set; }

        [TempData] public string Message { get; set; }

        [TempData] public string Code { get; set; }

        public async Task OnGet(string message = null, string code = null)
        {
            Message = message;
            Code = code;
            var result = await _departmentService.Load(0,10);
            Departments = result.ReturnData;
        }
    }
}