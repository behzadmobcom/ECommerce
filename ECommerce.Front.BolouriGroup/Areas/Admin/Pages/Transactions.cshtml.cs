using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages;


public class TransactionsModel : PageModel
{
    private ITransactionService TransactionService { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public TransactionsModel(ITransactionService transactionService)
    {
        TransactionService = transactionService;
    }

    public ServiceResult<List<Transaction>> Transactions { get; set; }

    public async Task OnGet()
    {
        Transactions = await TransactionService.Load();
    }
}