using FinTrack.Domain.Entities;
using FinTrack.Domain.Interfaces;

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
    }
}
