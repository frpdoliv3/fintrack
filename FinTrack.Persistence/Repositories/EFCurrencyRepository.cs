using FinTrack.Domain.Entities;
using FinTrack.Domain.Interfaces;
using FinTrack.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinTrack.Persistence.Repositories;

internal class EFCurrencyRepository : ICurrencyRepository
{
    private readonly FinTrackDbContext _context;

    public EFCurrencyRepository(FinTrackDbContext context) 
    {
        _context = context;
    }

    public async Task<Currency> AddCurrency(Currency currency)
    {
        _context.Currencies.Add(currency);
        await _context.SaveChangesAsync();
        return currency;
    }

    public async Task<bool> Exists(Expression<Func<Currency, bool>> predicate)
    {
        return await _context.Currencies.AnyAsync(predicate);
    }

    public async Task<Currency?> GetCurrencyById(uint id)
    {
        return await _context.Currencies.FindAsync(id);
    }

    public async Task<PagedList<Currency>> GetCurrencies(string searchQuery, int pageNumber, int pageSize)
    {
        var currencies = _context.Currencies.Where(
            c => c.Name.Contains(searchQuery)
        ).OrderBy(c => c.Id);
        
        return await PagedRepository<Currency>.PagedQuery(currencies, pageNumber, pageSize);
    }
}
