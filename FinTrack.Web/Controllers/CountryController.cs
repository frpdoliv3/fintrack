using FinTrack.Application.Country;
using FinTrack.Application.Country.CreateCountry;
using FinTrack.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly CountryService _countryService;

        public CountryController(CountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpPost]
        public async Task CreateCountry([FromBody] CreateCountryRequest createCountry)
        {
            await _countryService.AddCountry(createCountry);
        }

        [HttpGet]
        public IAsyncEnumerable<Country> ListCountries(
            [FromQuery(Name = "page_number")] int pageNumber = 1, 
            [FromQuery(Name = "page_size")] int pageSize = 10
        ) {
            return _countryService.ListCountries(pageNumber, pageSize);
        }
    }
}
