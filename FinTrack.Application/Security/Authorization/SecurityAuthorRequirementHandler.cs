using System.Security.Claims;
using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace FinTrack.Application.Security.Authorization;

public class SecurityAuthorRequirementHandler<T>
    : AuthorizationHandler<T, ulong> where T : IAuthorizationRequirement
{
    private readonly ISecurityRepository _securityRepo;
    
    public SecurityAuthorRequirementHandler(ISecurityRepository securityRepo) {
        _securityRepo = securityRepo;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        T requirement,
        ulong securityId
    ) {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) { return; }

        var hasAuthorship = await _securityRepo
            .ExistsAsync(s => s.Id == securityId && s.OwnerId == userId);
        if (hasAuthorship)
        {
            context.Succeed(requirement);
        }
    }
}
