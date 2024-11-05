using System.Linq.Expressions;
using FinTrack.Domain.Entities;

namespace FinTrack.Domain.Interfaces;
public interface ISecurityRepository
{
    public Task<Security> AddSecurity(Security security);
    public Task<Security?> GetSecurityById(ulong id);
    public Task<bool> Exists(Expression<Func<Security, bool>> predicate);
    public Task<PagedList<Operation>> GetOperationsForSecurity(ulong securityId, int pageNumber, int pageSize);
}
