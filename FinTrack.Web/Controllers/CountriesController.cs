using FinTrack.Application.Country.CreateCountry;
using FinTrack.Application.Security.GetSecurity;
using FinTrack.Domain.Entities;
using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Admin")]
public class CountriesController : ControllerBase
{
    private readonly ICountryRepository _countryRepository;

    private const string GET_COUNTRY_BY_ID_NAME = "GetCountryById";

    public CountriesController(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    [HttpPost]
    public async Task<IResult> CreateCountry([FromBody] CreateCountryRequest createCountry)
    {
        var createdCountry = await _countryRepository.AddCountry(createCountry.ToCountry());
        return Results.CreatedAtRoute(
            GET_COUNTRY_BY_ID_NAME, 
            new { id = createdCountry.Id },
            createdCountry
        );
    }

    [HttpGet("{id}", Name = GET_COUNTRY_BY_ID_NAME)]
    [ProducesResponseType(typeof(GetSecurityResponse), 200)]
    public async Task<IResult> GetCountryById(
        [FromRoute] uint id
    ) {
        var country = await _countryRepository.GetCountryById(id);
        if (country == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(country);
    }

    [HttpGet]
    public IAsyncEnumerable<Country> ListCountries(
        [FromQuery(Name = "page_number")] int pageNumber = 1, 
        [FromQuery(Name = "page_size")] int pageSize = 10
    ) {
        return _countryRepository.ListCountries(pageNumber, pageSize);
    }
}
