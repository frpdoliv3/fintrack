using FinTrack.Application.UseCases.CreateCountry;
using FinTrack.Domain.Interfaces;

namespace Application
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
    }
}
