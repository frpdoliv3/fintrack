using System.Security.Claims;

using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Entities = FinTrack.Domain.Entities;

namespace FinTrack.Application.Utils;

public abstract class AdminBypassAuthorizationHandler<R, T> : 
    AuthorizationHandler<R, T> where R : IAuthorizationRequirement
{
    private readonly IAuthRepository _authRepository;

    protected AdminBypassAuthorizationHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }
    
    protected abstract Task<bool> UserIsOwner(T entity, string userId);
    
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, R requirement, T resource)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) { return; }
        var isAdmin = await _authRepository.HasRole(userId, Entities.UserRole.Admin);
        if (await UserIsOwner(resource, userId) || isAdmin)
        {
            context.Succeed(requirement);
        }
    }
}
