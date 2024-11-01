using System.Security.Claims;
using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinTrack.Web.Filters;

public class LockoutAuthorizationActionPolicy: IAsyncAuthorizationFilter
{
    private readonly IAuthRepository _authRepo;
    private readonly ILogger<LockoutAuthorizationActionPolicy> _logger;
    
    public LockoutAuthorizationActionPolicy(ILogger<LockoutAuthorizationActionPolicy> logger, IAuthRepository authRepo)
    {
        _authRepo = authRepo;
        _logger = logger;
    }
    
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        _logger.LogInformation("Running lockout authorization policy (no checks)");
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
        {
            return;
        }
        var userIdFromClaim = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdFromClaim == null || !await _authRepo.ExistsWithId(userIdFromClaim))
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Result = new EmptyResult();
        }
    }
}