using ECommerce.Front.BolouriGroup.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Pages
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class MeliSuccessModel : PageModel
    {
        public IActionResult OnPost(PurchaseResult result)
        {
            if (!result.ResCode.Equals("0"))
            {
                var message = "";
                var code = "";
                switch (result.ResCode)
                {
                    case "100":
                        message = "درخواست تکراری است. قبلا در سیستم ثبت شده است";
                        break;
                    case "-1":
                        message = "پارامترهای ارسالی صحیح نمی باشد یا تراکنش در سیستم وجود ندارد";
                        break;
                    case "101":
                        message = "مهلت ارسال تراکنش به پایان رسیده است";
                        break;
                    default:
                        message = "لغو";
                        break;
                }
                code = "Error";
                return RedirectToPage("Checkout", new { message, code }); 
            }
            return RedirectToPage("invoice", result );
        }
    }
}
