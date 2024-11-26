using System.Security.Claims;
using FinTrack.Application.Security.Authorization;
using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace FinTrack.Application.Security.GetSecurityStatus;

public class GetSecurityStatusUseCase
{
    private readonly IAuthorizationService _authService;
    private readonly ISecurityRepository _securityRepo;
    
    public GetSecurityStatusUseCase(
        IAuthorizationService authService,
        ISecurityRepository securityRepo
    ) {
        _authService = authService;
        _securityRepo = securityRepo;
    }
    
    public async Task<GetSecurityStatusResponse> Execute(ClaimsPrincipal user, ulong securityId)
    {
        var authResult = await _authService
            .AuthorizeAsync(user, securityId, SecurityAuthorization.ViewSecurityPolicy);
        if (!authResult.Succeeded)
        {
            throw new UnauthorizedAccessException();
        }

        var operations = _securityRepo.GetOperationsForSecurity(securityId);
        if (!await OperationOrderValidator.ValidateOperations(operations))
        {
            return new GetSecurityStatusResponse(SecurityStatus.InvalidOperationOrder);
        }
        return new GetSecurityStatusResponse(SecurityStatus.Ok);
    }
}