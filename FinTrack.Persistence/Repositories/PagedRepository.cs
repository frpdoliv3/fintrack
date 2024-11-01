using FinTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Persistence.Repositories;

internal static class PagedRepository<T>
{
    public static async Task<PagedList<T>> PagedQuery(IQueryable<T> query, int page, int pageSize)
    {
        if (page < 1) { page = 1; }

        if (pageSize > 100) { pageSize = 100; }
        var totalCount = await query.CountAsync();
        var results = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return new PagedList<T>
        {
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            Items = results
        };
    }
}