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
        var createdSecurity = await _context.Securities.AddAsync(security);
        await _context.SaveChangesAsync();
        return createdSecurity.Entity;
    }
    
    public async Task<Security?> GetSecurityById(ulong id)
    {
        return await _context.Securities
            .FindAsync(id);
    }

    public async Task<Security> UpdateSecurity(Security security)
    {
        await _context.SaveChangesAsync();
        return security;
    }

    public async Task<PagedList<Operation>> GetOperationsForSecurity(ulong securityId, int pageNumber, int pageSize)
    {
        var operations = _context.Operations
            .Where(o => o.Security.Id == securityId)
            .OrderBy(o => o.OperationDate);
        
        return await PagedRepository<Operation>.PagedQuery(operations, pageNumber, pageSize);
    }

    public IAsyncEnumerable<Operation> GetOperationsForSecurity(ulong securityId)
    {
        return _context.Operations
            .Where(o => o.Security.Id == securityId)
            .OrderBy(o => o.OperationDate)
            .AsAsyncEnumerable();
    }
    
    public async Task<bool> ExistsAsync(Expression<Func<Security, bool>> predicate)
    {
        return await _context.Securities.AnyAsync(predicate);
    }

    public bool Exists(Expression<Func<Security, bool>> predicate)
    {
        return _context.Securities.Any(predicate);
    }

    public async Task<Operation?> GetOperationById(ulong operationId)
    {
        return await _context.Operations
            .Include(o => o.Security)
            .Where(o => o.Id == operationId)
            .FirstOrDefaultAsync();
    }
    
    public async Task DeleteOperation(Operation operation)
    {
        _context.Operations.Remove(operation);
        await _context.SaveChangesAsync();
    }
}
