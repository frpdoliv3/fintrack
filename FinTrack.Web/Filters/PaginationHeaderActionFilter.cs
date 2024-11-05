
using FinTrack.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.WebUtilities;

namespace FinTrack.Web.Filters;

public class PaginationHeaderActionFilter: IActionFilter
{
    private readonly ILogger<PaginationHeaderActionFilter> _logger;
    
    public PaginationHeaderActionFilter(ILogger<PaginationHeaderActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context) 
    {}

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is not OkObjectResult result)
        {
            return;
        }

        if (result.Value is not IPagedList paginationContents) { return; }
        context.Result = new OkObjectResult(paginationContents.Items);
        // There is only one page in the pagination
        if (paginationContents is { HasPreviousPage: false, HasNextPage: false })
        {
            return;
        }
        context.HttpContext.Response.Headers.Link =
            CreateLinkHeader(context.HttpContext.Request, paginationContents);
    }

    private string CreateLinkHeader(HttpRequest request, IPagedList paginationContents)
    {
        var paginationLinks = new List<string>();
        if (paginationContents.HasPreviousPage)
        {
            var prevPage = paginationContents.Page - 1 <= paginationContents.LastPage ?
                paginationContents.Page - 1 : paginationContents.LastPage;
            var prevPageLink = CreateLinkHeader(request, prevPage);
            var firstPageLink = CreateLinkHeader(request, 1);
            paginationLinks.Add($"<{firstPageLink}>; rel=\"first\"");
            paginationLinks.Add($"<{prevPageLink}>; rel=\"prev\"");
        }

        if (paginationContents.HasNextPage)
        {
            var nextPageLink = CreateLinkHeader(request, paginationContents.Page + 1);
            var lastPageLink = CreateLinkHeader(request, paginationContents.LastPage);
            paginationLinks.Add($"<{nextPageLink}>; rel=\"next\"");
            paginationLinks.Add($"<{lastPageLink}>; rel=\"last\"");
        }
        return string.Join(", ", paginationLinks);
    }

    private string CreateLinkHeader(HttpRequest request, int pageNumber)
    {
        var queryParams = request.Query.ToDictionary(
            k => k.Key,
            v => (string?) v.Value.ToString()
        );
        queryParams["page"] = pageNumber.ToString();
        var httpPath = $"{request.Scheme}://{request.Host}{request.Path}";
        var fullRequestPath = QueryHelpers.AddQueryString(httpPath, queryParams);
        return fullRequestPath;
    }
}