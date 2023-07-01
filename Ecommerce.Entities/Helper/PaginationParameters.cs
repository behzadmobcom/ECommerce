namespace Ecommerce.Entities.Helper;

public class PaginationParameters
{
    private const int MaxPageSize = 50;
    private int _pageSize = 10;
    public int PageNumber { get; set; } = 1;
    public string? Search { get; set; }
    public string? TagText { get; set; }
    public int CategoryId { get; set; }
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
}

public class PaginationDetails
{
    public string? Search { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }
    public string? Address { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public bool isCheckExist { get; set; }
    public int ProductSort { get; set; }
    public bool? IsPaid { get; set; }
    public string Username { get; set; }
    public int UserId { get; set; }
    public Status Status { get; set; }
    public PurchaseSort PurchaseSort { get; set; }
}