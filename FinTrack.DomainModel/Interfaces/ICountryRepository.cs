using FinTrack.Domain.Entities;

namespace FinTrack.Domain.Interfaces
{
    public interface ICountryRepository
    {
        public Task AddCountry(Country country);
        public IAsyncEnumerable<Country> ListCountries(int pageNumber, int pageSize);
    }
}
