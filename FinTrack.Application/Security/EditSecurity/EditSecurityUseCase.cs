using System.Security.Claims;
using FinTrack.Application.Security.Authorization;
using FinTrack.Application.Security.CreateSecurity;
using FinTrack.Application.Security.GetSecurity;
using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace FinTrack.Application.Security.EditSecurity;

public class EditSecurityUseCase
{
    private readonly IAuthorizationService _authService;
    private readonly SecurityMapper _securityMapper;
    private readonly ISecurityRepository _securityRepo;
    
    public EditSecurityUseCase(
        IAuthorizationService authService,
        SecurityMapper securityMapper,
        ISecurityRepository securityRepo
    ) {
        _authService = authService;
        _securityMapper = securityMapper;
        _securityRepo = securityRepo;
    }
    
    public async Task<GetSecurityResponse?> Execute(ClaimsPrincipal user, EditSecurityRequest editSecurity)
    {
        var security = await _securityRepo.GetSecurityById(editSecurity.Id);
        if (security == null) { return null; }
        
        var authResult = await _authService
            .AuthorizeAsync(user, security.Id, SecurityAuthorization.ChangeSecurityPolicy);
        if (!authResult.Succeeded) { return null; }

        var mappedSecurity = await _securityMapper.UpdateSecurity(editSecurity, security);
        var updatedSecurity = await _securityRepo.UpdateSecurity(mappedSecurity);
        return _securityMapper.ToGetSecurityResponse(updatedSecurity);
    }
}