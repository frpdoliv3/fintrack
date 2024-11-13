using System.Security.Claims;
using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Entities = FinTrack.Domain.Entities;

namespace FinTrack.Application.Utils.Authorization;

public class AdminRequirementHandler<T>: AuthorizationHandler<T> where T : IAuthorizationRequirement
{
    private readonly IAuthRepository _authRepo;

    public AdminRequirementHandler(IAuthRepository authRepo)
    {
        _authRepo = authRepo;
    }
    
    
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, T requirement)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) { return; }

        var isAdmin = await _authRepo.HasRole(userId, Entities.UserRole.Admin);
        if (isAdmin)
        {
            context.Succeed(requirement);
        }
    }
}