using System.Linq.Expressions;
using FinTrack.Domain.Entities;
using FinTrack.Domain.Interfaces;
using FinTrack.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Persistence.Repositories;
internal class EFSecurityRepository: ISecurityRepository
{
    private readonly FinTrackDbContext _context;

    public EFSecurityRepository(FinTrackDbContext context)
    {
        _context = context;
    }
    
    public async Task<Security> AddSecurity(Security security)
    {
        await _context.Securities.AddAsync(security);
        await _context.SaveChangesAsync();
        return security;
    }
    
    public async Task<Security?> GetSecurityById(ulong id)
    {
        return await _context.Securities
            .FindAsync(id);
    }

    public async Task<PagedList<Operation>> GetOperationsForSecurity(ulong securityId, int pageNumber, int pageSize)
    {
        var operations = _context.Operations
            .Where(o => o.Security.Id == securityId)
            .IgnoreAutoIncludes()
            .OrderBy(o => o.OperationDate);
        
        return await PagedRepository<Operation>.PagedQuery(operations, pageNumber, pageSize);
    }
    
    public async Task<bool> Exists(Expression<Func<Security, bool>> predicate)
    {
        return await _context.Securities
            .AnyAsync(predicate);
    }
}
