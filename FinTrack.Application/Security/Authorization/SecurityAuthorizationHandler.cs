using FinTrack.Application.Utils;
using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Entities = FinTrack.Domain.Entities;

namespace FinTrack.Application.Security.Authorization;

public class SecurityAuthorizationHandler 
    : AdminBypassAuthorizationHandler<SecurityAuthorization.SameAuthorRequirement, ulong>
{
    private readonly ISecurityRepository _securityRepo;
    
    public SecurityAuthorizationHandler(
        IAuthRepository authRepo,
        ISecurityRepository securityRepo
    ) : base(authRepo) {
        _securityRepo = securityRepo;
    }

    protected override async Task<bool> UserIsOwner(ulong securityId, string userId)
    {
        return await _securityRepo
            .Exists(s => s.Id == securityId && s.OwnerId == userId);
    }
}
