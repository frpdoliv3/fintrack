using FinTrack.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace FinTrack.Server;

public class SecurityAuthorizationCrudHandler : 
    AuthorizationHandler<OperationAuthorizationRequirement, Security>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Security resource)
    {
        if (context.User.Identity == resource.Author)
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}

