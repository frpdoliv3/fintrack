using System.Security.Claims;
using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace FinTrack.Application.Security.Authorization;

public class SecurityAuthorRequirementHandler 
    : AuthorizationHandler<SecurityAuthorization.AuthorRequirement, ulong>
{
    private readonly ISecurityRepository _securityRepo;
    
    public SecurityAuthorRequirementHandler(ISecurityRepository securityRepo) {
        _securityRepo = securityRepo;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        SecurityAuthorization.AuthorRequirement requirement,
        ulong securityId
    ) {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) { return; }

        var hasAuthorship = await _securityRepo
            .Exists(s => s.Id == securityId && s.OwnerId == userId);
        if (hasAuthorship)
        {
            context.Succeed(requirement);
        }
    }
}
