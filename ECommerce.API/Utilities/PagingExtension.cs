using Entities;
using Entities.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace API.Utilities;

public static class PagingExtension
{
    public static async Task<List<T>> Paginate<T>(this IQueryable<T> queryable,
        PageViewModel pageViewModel) where T : BaseEntity
    {
        return await queryable.OrderByDescending(x => x.Id)
            .Skip((pageViewModel.Page - 1) * pageViewModel.QuantityPerPage)
            .Take(pageViewModel.QuantityPerPage).ToListAsync();
    }
}

public class PagedList<T> : List<T>
{
    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int) Math.Ceiling(count / (double) pageSize);
        AddRange(items);
    }

    public int CurrentPage { get; }
    public int TotalPages { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public static PagedList<T> ToPagedList(List<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count;
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}