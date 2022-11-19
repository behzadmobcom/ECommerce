namespace Entities.Helper;

public class PaginationParameters
{
    private const int MaxPageSize = 50;
    private int _pageSize = 10;
    public int PageNumber { get; set; } = 1;
    public string? Search { get; set; }
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
    public int MinPrice { get; set; }
    public int MaxPrice { get; set; }
    public bool isExist { get; set; }
    public int ProductSort { get; set; }
}