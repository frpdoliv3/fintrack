using System.Security.Claims;
using FinTrack.Application.Security.Authorization;
using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace FinTrack.Application.Security;

public class DeleteSecurityUseCase
{
    private readonly IAuthorizationService _authService;
    private readonly ISecurityRepository _securityRepo;
    
    public DeleteSecurityUseCase(
        IAuthorizationService authService,
        ISecurityRepository securityRepo
    ) {
          _authService = authService;
          _securityRepo = securityRepo;
    }
    
    public async Task<bool> Execute(ClaimsPrincipal user, ulong securityId)
    {
        var authResult = await _authService
            .AuthorizeAsync(user, securityId, SecurityAuthorization.ChangeSecurityPolicy);

        if (!authResult.Succeeded) { return false; }

        var security = await _securityRepo.GetSecurityById(securityId);
        if (security == null) { return false; }

        await _securityRepo.DeleteSecurity(security);
        return true;
    }
}