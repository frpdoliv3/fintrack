using FinTrack.Application.Country.CreateCountry;
using FinTrack.Domain.Interfaces;
using Entities = FinTrack.Domain.Entities;

namespace FinTrack.Application.Country
{
    public class CountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task AddCountry(CreateCountryRequest createCountry)
        {
            var country = createCountry.ToCountry();

            await _countryRepository.AddCountry(country);
        }

        public IAsyncEnumerable<Entities.Country> ListCountries(int pageNumber, int pageSize)
        {
            return _countryRepository.ListCountries(pageNumber, pageSize);
        }

        internal async Task<bool> ExistsName(string countryName)
        {
            return await _countryRepository.ExistsName(countryName);
        }
        internal async Task<bool> ExistsAlpha2Code(string alpha2Code)
        {
            return await _countryRepository.ExistsAlpha2Code(alpha2Code);
        }
        internal async Task<bool> ExistsAlpha3Code(string alpha3Code)
        {
            return await _countryRepository.ExistsAlpha3Code(alpha3Code);
        }
        internal async Task<bool> ExistsNumericCode(int numericCode)
        {
            return await _countryRepository.ExistsNumericCode(numericCode);
        }
    }
}
