using Application;
using FinTrack.Application.UseCases.CreateCountry;
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
        public void CreateCountry([FromBody] CreateCountryRequest createCountry)
        {
            _countryService.AddCountry(createCountry);
        }
    }
}
