using System.Linq.Expressions;
using FinTrack.Domain.Entities;

namespace FinTrack.Domain.Interfaces;
public interface ISecurityRepository
{
    public Task<Security> AddSecurity(Security security);
    public Task<bool> Exists(Expression<Func<Security, bool>> predicate);
}
