using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

using Entities = FinTrack.Domain.Entities;

namespace FinTrack.Application.Operation.Authorization;

public class OperationAuthorizationHandler<T>
    : AuthorizationHandler<T, Entities.Operation> where T : IAuthorizationRequirement
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        T requirement,
        Entities.Operation resource
    ) {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) { return Task.CompletedTask; }

        if (resource.Security.OwnerId == userId)
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}