using System.Security.Claims;
using FinTrack.Application.Utils;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinTrack.Web;

public class UserIdActionFilter: IActionFilter
{
    private readonly ILogger<UserIdActionFilter> _logger;
    
    public UserIdActionFilter(ILogger<UserIdActionFilter> logger)
    {
        _logger = logger;
    }
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("Running user id action filter (no checks)");
        var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument is IHasOwnerId ownerIdArgument)
            {
                ownerIdArgument.OwnerId = userId;
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    { }
}