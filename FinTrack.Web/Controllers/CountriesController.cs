using FinTrack.Application.Country.CreateCountry;
using FinTrack.Application.Security.GetSecurity;
using FinTrack.Domain.Entities;
using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CountriesController : ControllerBase
{
    private readonly ICountryRepository _countryRepository;

    private const string GetCountryByIdName = "GetCountryById";

    public CountriesController(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    [HttpPost]
    [Authorize(Roles = UserRole.Admin)]
    public async Task<IActionResult> CreateCountry([FromBody] CreateCountryRequest createCountry)
    {
        var createdCountry = await _countryRepository.AddCountry(createCountry.ToCountry());
        
        return CreatedAtRoute(
            GetCountryByIdName, 
            new { id = createdCountry.Id },
            createdCountry
        );
    }

    [HttpGet("{id}", Name = GetCountryByIdName)]
    [ProducesResponseType(typeof(GetSecurityResponse), 200)]
    public async Task<IActionResult> GetCountryById(
        [FromRoute] uint id
    ) {
        var country = await _countryRepository.GetCountryById(id);
        if (country == null)
        {
            return NotFound();
        }
        return Ok(country);
    }

    [HttpGet]
    public async Task<IActionResult> GetCountries(
        [FromQuery(Name = "search_query")] string searchQuery = "",
        [FromQuery(Name = "page")] int pageNumber = 1, 
        [FromQuery(Name = "page_size")] int pageSize = 10
    ) {
        var countries = await _countryRepository.GetCountries(searchQuery, pageNumber, pageSize);
        return Ok(countries);
    }
}
