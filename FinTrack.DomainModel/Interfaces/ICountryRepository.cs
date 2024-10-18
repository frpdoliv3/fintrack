using FinTrack.Domain.Entities;

namespace FinTrack.Domain.Interfaces
{
    public interface ICountryRepository
    {
        public Task AddCountry(Country country);
        public IAsyncEnumerable<Country> ListCountries(int pageNumber, int pageSize);
        public Task<bool> ExistsName(string countryName);
        public Task<bool> ExistsAlpha2Code(string alpha2Code);
        public Task<bool> ExistsAlpha3Code(string alpha3Code);
    }
}
