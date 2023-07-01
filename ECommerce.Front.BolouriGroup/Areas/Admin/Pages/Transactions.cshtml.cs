using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages;


public class TransactionsModel : PageModel
{
    private readonly IUserService _userService;
    private ITransactionService TransactionService { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }
    public TransactionsModel(ITransactionService transactionService)
    {
        TransactionService = transactionService;
    }

    public ServiceResult<List<Transaction>> Transactions { get; set; } = new ServiceResult<List<Transaction>>();

    public async Task<IActionResult> OnGet(int userId = 0, string userName = "", string search = "", int pageNumber = 1, int pageSize = 10,
    string message = null, string code = null, decimal? minimumAmount = null, decimal? maximumAmount = null,
    Status status = Ecommerce.Entities.Status.New, PurchaseSort purchaseSort = PurchaseSort.HighToLowDateBuying)
    {
        var result = await TransactionService.Load(userId, userName, search, pageNumber, pageSize, message, code, minimumAmount
            , maximumAmount, status, purchaseSort);
        if (result.Code == ServiceCode.Success)
        {
            Message = result.Message;
            Code = result.Code.ToString();
            Transactions = result;
            Transactions.PaginationDetails.UserId = userId;
            Transactions.PaginationDetails.Username = userName;
            Transactions.PaginationDetails.Search = search;
            Transactions.PaginationDetails.MinPrice = minimumAmount;
            Transactions.PaginationDetails.MaxPrice = maximumAmount;
            Transactions.PaginationDetails.Status = status;
            Transactions.PaginationDetails.PurchaseSort = purchaseSort;
            Transactions.PaginationDetails.PageSize = pageSize;
            return Page();
        }

        return RedirectToPage("/index", new { message = result.Message, code = result.Code.ToString() });
    }
    public async Task<IActionResult> OnPost(ServiceResult<List<Transaction>> Transactions)
    {
        try
        {
            return RedirectToPage("/Transactions",
                    new
                    {
                        area = "Admin",
                        minimumAmount = Transactions.PaginationDetails.MinPrice,
                        maximumAmount = Transactions.PaginationDetails.MaxPrice,
                        status = Transactions.PaginationDetails.Status,
                        purchaseSort = Transactions.PaginationDetails.PurchaseSort,
                        userName = Transactions.PaginationDetails.Username,
                        userId = Transactions.PaginationDetails.UserId,
                        pageSize = 10
                    });
        }
        catch (Exception ex)
        {
            return Page();
        }
    }
    public async Task<JsonResult> OnGetUserListBySearch(string search)
    {
        var result = await _userService.UserList(search: search);
        List<string> users = result.ReturnData.Select(x => x.Username).ToList();
        return new JsonResult(users);
    }
}