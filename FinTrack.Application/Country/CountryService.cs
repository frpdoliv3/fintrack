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

        public void AddCountry(CreateCountryRequest createCountry)
        {
            var country = createCountry.ToCountry();

            _countryRepository.AddCountry(country);
        }

        public IAsyncEnumerable<Entities.Country> ListCountries(int pageNumber, int pageSize)
        {
            return _countryRepository.ListCountries(pageNumber, pageSize);
        }
    }
}
