using FinTrack.Application.Security.CreateSecurity;
using FinTrack.Domain.Interfaces;

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

    public async Task AddSecurity(CreateSecurityRequest createSecurityRequest)
    {
        await _securityRepo
            .AddSecurity(await _createSecurityMapper.ToSecurity(createSecurityRequest));
    }
}