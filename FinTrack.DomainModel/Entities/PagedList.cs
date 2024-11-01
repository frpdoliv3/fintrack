using System.Collections;

namespace FinTrack.Domain.Entities;

public record PagedList<T>: IPagedList
{
    IEnumerable IPagedList.Items => Items;
    public List<T> Items { get; init; } = null!;
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
}
