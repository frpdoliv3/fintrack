using System.Security.Claims;
using FinTrack.Application.Security.Authorization;
using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Entities = FinTrack.Domain.Entities;

namespace FinTrack.Application.Security.GetOperations;

public class GetOperationsForIdUseCase
{
    private readonly IAuthorizationService _authService;
    private readonly ISecurityRepository _securityRepo;
    
    public GetOperationsForIdUseCase(
        IAuthorizationService authService,
        ISecurityRepository securityRepo
    ) {
        _authService = authService;
        _securityRepo = securityRepo;
    }
    
    public async Task<Entities.PagedList<Entities.Operation>> Execute(
        ClaimsPrincipal user,
        ulong securityId,
        int pageNumber,
        int pageSize
    ) {
        var authResult = await _authService
            .AuthorizeAsync(user, securityId, SecurityAuthorization.ViewSecurityPolicy);
        if (!authResult.Succeeded)
        {
            throw new UnauthorizedAccessException();
        }
        return await _securityRepo.GetOperationsForSecurity(securityId, pageNumber, pageSize);
    }
}