using FinTrack.Domain.Entities;
using FinTrack.Domain.Interfaces;
using FinTrack.Persistence.Contexts;

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
}
