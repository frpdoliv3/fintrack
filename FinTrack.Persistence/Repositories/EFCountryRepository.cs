using FinTrack.Domain.Entities;
using FinTrack.Domain.Interfaces;
using FinTrack.Persistence.Context;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> ExistsName(string countryName)
        {
            return await _context.Countries.AnyAsync(x => x.Name == countryName);
        }

        public async Task<bool> ExistsAlpha2Code(string alpha2Code)
        {
            return await _context.Countries.AnyAsync(x => x.Alpha2Code == alpha2Code);
        }

        public async Task<bool> ExistsAlpha3Code(string alpha3Code)
        {
            return await _context.Countries.AnyAsync(x => x.Alpha3Code == alpha3Code);
        }

        public async Task<bool> ExistsNumericCode(int numericCode)
        {
            return await _context.Countries.AnyAsync(x => x.NumericCode == numericCode);
        }
    }
}
