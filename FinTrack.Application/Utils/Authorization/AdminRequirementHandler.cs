using System.Security.Claims;
using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Entities = FinTrack.Domain.Entities;

namespace FinTrack.Application.Utils.Authorization;

public class AdminRequirementHandler: AuthorizationHandler<AdminRequirement>
{
    private readonly IAuthRepository _authRepository;

    protected AdminRequirementHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }
    
    
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) { return; }

        var isAdmin = await _authRepository.HasRole(userId, Entities.UserRole.Admin);
        if (isAdmin)
        {
            context.Succeed(requirement);
        }
    }
}