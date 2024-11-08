using FinTrack.Application.Security.CreateSecurity;
using FinTrack.Application.Security.GetSecurity;
using FinTrack.Application.Security.GetSecurityStatus;
using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Entities = FinTrack.Domain.Entities;
using UnauthorizedAccessException = FinTrack.Domain.Exceptions.UnauthorizedAccessException;

namespace FinTrack.Application.Security;

public class SecurityService
{
    private readonly SecurityMapper _securityMapper;
    private readonly ISecurityRepository _securityRepo;
    
    public SecurityService(SecurityMapper securityMapper, ISecurityRepository securityRepo)
    {
        _securityMapper = securityMapper;
        _securityRepo = securityRepo;
    }

    public async Task<GetSecurityResponse> AddSecurity(CreateSecurityRequest createSecurityRequest)
    {
        var domainSecurity = await _securityMapper.ToSecurity(createSecurityRequest); 
        var createdSecurity = await _securityRepo.AddSecurity(domainSecurity);
        return _securityMapper.ToGetSecurityResponse(createdSecurity);
    }

    public async Task<GetSecurityResponse?> GetSecurityById(ulong securityId, string userId)
    {
        var domainSecurity = await _securityRepo.GetSecurityById(securityId);
        if (domainSecurity == null || domainSecurity.OwnerId != userId)
        {
            return null;
        }
        return _securityMapper.ToGetSecurityResponse(domainSecurity);
    }

    public async Task<Entities.PagedList<Entities.Operation>> GetOperationsForId(
        string ownerId,
        ulong securityId,
        int pageNumber,
        int pageSize
    ) {
        if (!await ValidSecurityOwnership(ownerId, securityId))
        {
            throw new UnauthorizedAccessException();
        }
        return await _securityRepo.GetOperationsForSecurity(securityId, pageNumber, pageSize);
    }

    public async Task<GetSecurityStatusResponse> GetOperationStatus(string ownerId, ulong securityId)
    {
        if (!await ValidSecurityOwnership(ownerId, securityId))
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

    private async Task<bool> ValidSecurityOwnership(string ownerId, ulong securityId)
    {
        var existsForId = await _securityRepo.Exists(s => s.OwnerId == ownerId && s.Id == securityId);
        return existsForId;
    }
}