using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Sadin.Common.Utilities;

public class PaginatedList<T> : List<T>
{
    protected PaginatedList()
    {
    }
    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }
    public int TotalItems { get; private set; }
    public bool ShowPagination { get; private set; }

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalItems = count;
        ShowPagination = (count > pageSize);
        this.AddRange(items);
    }
    public bool HasPreviousPage => (PageIndex > 1);

    public bool HasNextPage => (PageIndex < TotalPages);

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize,Expression<Func<T, bool>>? where = null,
        string orderBy = "", bool desc = false, CancellationToken cancellationToken = default(CancellationToken))
    {
        where ??= x => true;
            
        if(orderBy.HasValue())
        {
            source = desc ? source.OrderByDescending<T>(orderBy) : source.OrderBy<T>(orderBy);
        }
            
        var count = await source.CountAsync(where, cancellationToken);
        var items = await source.Where(where).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }

    public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize,Expression<Func<T, bool>>? where = null,
        string orderBy = "", bool desc = false)
    {
        where ??= x => true;
            
        if(orderBy.HasValue())
        {
            source = desc ? source.OrderByDescending<T>(orderBy) : source.OrderBy<T>(orderBy);
        }
            
            
        var count = source.Count(where);
        var items = source.Where(where).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }
    
}