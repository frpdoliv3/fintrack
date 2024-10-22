using FinTrack.Application.Country;
using FinTrack.Application.Country.CreateCountry;
using FinTrack.Domain.Entities;
using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CountryController : ControllerBase
{
    private readonly ICountryRepository _countryRepository;

    public CountryController(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    [HttpPost]
    public async Task CreateCountry([FromBody] CreateCountryRequest createCountry)
    {
        await _countryRepository.AddCountry(createCountry.ToCountry());
    }

    [HttpGet]
    public IAsyncEnumerable<Country> ListCountries(
        [FromQuery(Name = "page_number")] int pageNumber = 1, 
        [FromQuery(Name = "page_size")] int pageSize = 10
    ) {
        return _countryRepository.ListCountries(pageNumber, pageSize);
    }
}
