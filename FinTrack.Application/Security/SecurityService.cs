using System.Security.Claims;
using FinTrack.Application.Security.Authorization;
using FinTrack.Application.Security.CreateSecurity;
using FinTrack.Application.Security.EditSecurity;
using FinTrack.Application.Security.GetSecurity;
using FinTrack.Application.Security.GetSecurityStatus;
using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Entities = FinTrack.Domain.Entities;
using UnauthorizedAccessException = FinTrack.Domain.Exceptions.UnauthorizedAccessException;

namespace FinTrack.Application.Security;

public class SecurityService
{
    private readonly SecurityMapper _securityMapper;
    private readonly ISecurityRepository _securityRepo;
    private readonly IAuthorizationService _authService;
    private readonly ICountryRepository _countryRepo;
    private readonly ICurrencyRepository _currencyRepo;
    
    
    public SecurityService(
        IAuthorizationService authService,
        SecurityMapper securityMapper,
        ISecurityRepository securityRepo,
        ICountryRepository countryRepo,
        ICurrencyRepository currencyRepo
    ) {
        _securityMapper = securityMapper;
        _securityRepo = securityRepo;
        _authService = authService;
        _countryRepo = countryRepo;
        _currencyRepo = currencyRepo;
    }

    public async Task<GetSecurityResponse> AddSecurity(CreateSecurityRequest createSecurityRequest)
    {
        var domainSecurity = await _securityMapper.ToSecurity(createSecurityRequest); 
        var createdSecurity = await _securityRepo.AddSecurity(domainSecurity);
        return _securityMapper.ToGetSecurityResponse(createdSecurity)!;
    }

    public async Task<GetSecurityResponse?> GetSecurityById(ClaimsPrincipal user, ulong securityId)
    {
        var authResult = await _authService
            .AuthorizeAsync(user, securityId, SecurityAuthorization.ViewSecurityPolicy);
        if (!authResult.Succeeded) { return null; }
        var domainSecurity = await _securityRepo.GetSecurityById(securityId);
        return authResult.Succeeded ? _securityMapper.ToGetSecurityResponse(domainSecurity) : null;
    }

    public async Task<Entities.PagedList<Entities.Operation>> GetOperationsForId(
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

    public async Task<GetSecurityStatusResponse> GetOperationStatus(ClaimsPrincipal user, ulong securityId)
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

    public async Task<GetSecurityResponse?> UpdateSecurity(ClaimsPrincipal user, EditSecurityRequest editSecurity)
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

    public async Task<bool> DeleteSecurity(ClaimsPrincipal user, ulong securityId)
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
