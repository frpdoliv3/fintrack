using FinTrack.Application.Security.GetSecurity;
using FinTrack.Domain.Interfaces;

namespace FinTrack.Application.Security.CreateSecurity;

public class CreateSecurityUseCase
{
    private readonly SecurityMapper _securityMapper;
    private readonly ISecurityRepository _securityRepo;
    
    public CreateSecurityUseCase(
        SecurityMapper securityMapper,
        ISecurityRepository securityRepo
    )
    {
        _securityMapper = securityMapper;
        _securityRepo = securityRepo;
    }
    
    public async Task<GetSecurityResponse> Execute(CreateSecurityRequest createSecurityRequest)
    {
        var domainSecurity = await _securityMapper.ToSecurity(createSecurityRequest); 
        var createdSecurity = await _securityRepo.AddSecurity(domainSecurity);
        return _securityMapper.ToGetSecurityResponse(createdSecurity)!;
    }
}