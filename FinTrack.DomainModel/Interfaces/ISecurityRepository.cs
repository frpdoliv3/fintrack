using FinTrack.Domain.Entities;

namespace FinTrack.Domain.Interfaces;
public interface ISecurityRepository
{
    public Task<Security> AddSecurity(Security security); 
}
