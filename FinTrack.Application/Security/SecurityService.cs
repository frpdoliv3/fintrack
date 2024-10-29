using FinTrack.Application.Security.CreateSecurity;
using FinTrack.Domain.Interfaces;
using Entities = FinTrack.Domain.Entities;

namespace FinTrack.Application.Security;

public class SecurityService
{
    private readonly CreateSecurityMapper _createSecurityMapper;
    private readonly ISecurityRepository _securityRepo;
    
    public SecurityService(CreateSecurityMapper createSecurityMapper, ISecurityRepository securityRepo)
    {
        _createSecurityMapper = createSecurityMapper;
        _securityRepo = securityRepo;
    }

    public async Task<CreateSecurityResponse> AddSecurity(CreateSecurityRequest createSecurityRequest)
    {
        var domainSecurity = await _createSecurityMapper.ToSecurity(createSecurityRequest); 
        var createdSecurity = await _securityRepo.AddSecurity(domainSecurity);
        return _createSecurityMapper.ToCreateSecurityResponse(createdSecurity);
    }
}