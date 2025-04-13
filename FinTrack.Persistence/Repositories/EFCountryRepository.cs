using FinTrack.Domain.Entities;
using FinTrack.Domain.Interfaces;
using FinTrack.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinTrack.Infrastructure.Repositories;

internal class EFCountryRepository : ICountryRepository
{
    private readonly FinTrackDbContext _context;

    public EFCountryRepository(FinTrackDbContext context)
    {
        _context = context;
    }

    public async Task<Country> AddCountry(Country country)
    {
        _context.Countries.Add(country);
        await _context.SaveChangesAsync();

        return country;
    }
    
    public async Task<PagedList<Country>> GetCountries(string searchQuery, int pageNumber, int pageSize)
    {
        var countries = _context.Countries.Where(
            c => c.Name.Contains(searchQuery)
        ).OrderBy(c => c.Id);
        
        return await PagedRepository<Country>.PagedQuery(countries, pageNumber, pageSize);
    }

    public bool Exists(Expression<Func<Country, bool>> predicate)
    {
        return _context.Countries.Any(predicate);
    }

    public async Task<Country?> GetCountryById(uint id)
    {
        return await _context.Countries.FindAsync(id);
    }
}
