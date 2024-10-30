using FinTrack.Application.Security.CreateSecurity;
using FinTrack.Application.Security.GetSecurity;
using FinTrack.Domain.Interfaces;
using Entities = FinTrack.Domain.Entities;

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

    public async Task<GetSecurityResponse?> GetSecurityById(ulong securityId)
    {
        var domainSecurity = await _securityRepo.GetSecurityById(securityId);
        return domainSecurity == null ? null : _securityMapper.ToGetSecurityResponse(domainSecurity);
    }
}