using System.Security.Claims;
using FinTrack.Application.Security.Authorization;
using FinTrack.Application.Security.CreateSecurity;
using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace FinTrack.Application.Security.GetSecurity;

public class GetSecurityByIdUseCase
{
    private readonly IAuthorizationService _authService;
    private readonly ISecurityRepository _securityRepo;
    private readonly SecurityMapper _securityMapper;

    public GetSecurityByIdUseCase(
        IAuthorizationService authService,
        ISecurityRepository securityRepo,
        SecurityMapper securityMapper
    )
    {
        _authService = authService;
        _securityRepo = securityRepo;
        _securityMapper = securityMapper;
    }
    
    public async Task<GetSecurityResponse?> Execute(ClaimsPrincipal user, ulong securityId)
    {
        var authResult = await _authService
            .AuthorizeAsync(user, securityId, SecurityAuthorization.ViewSecurityPolicy);
        if (!authResult.Succeeded) { return null; }
        var domainSecurity = await _securityRepo.GetSecurityById(securityId);
        return authResult.Succeeded ? _securityMapper.ToGetSecurityResponse(domainSecurity) : null;
    }
}