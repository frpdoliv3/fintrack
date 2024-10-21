

using FinTrack.Domain.Entities;
using FinTrack.Domain.Interfaces;
using FinTrack.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinTrack.Persistence.Repositories
{
    internal class EFCountryRepository: ICountryRepository
    {
        private readonly FinTrackDbContext _context;

        public EFCountryRepository(FinTrackDbContext context)
        {
            _context = context;
        }

        public async Task AddCountry(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
        }
        
        public IAsyncEnumerable<Country> ListCountries(int pageNumber, int pageSize)
        {
            return _context.Countries
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .AsAsyncEnumerable();
        }

        public async Task<bool> Exists(Expression<Func<Country, bool>> predicate)
        {
            return await _context.Countries.AnyAsync(predicate);
        }
    }
}
