using System.Collections;

namespace FinTrack.Domain.Entities;

public interface IPagedList
{
    IEnumerable Items { get; }
    int Page { get;  }
    int PageSize { get;  }
    int TotalCount { get; }
    int LastPage => (int) Math.Ceiling((double)TotalCount / PageSize);
    bool HasNextPage => Page * PageSize < TotalCount;
    bool HasPreviousPage => Page > 1;
}