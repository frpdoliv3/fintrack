using FinTrack.Application.Currency.CreateCurrency;
using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Admin")]
public class CurrenciesController : ControllerBase
{
    private readonly ICurrencyRepository _currencyRepo;

    private const string GetCurrencyByIdName = "GetCurrencyById";

    public CurrenciesController(ICurrencyRepository currencyRepo)
    {
        _currencyRepo = currencyRepo;
    }

    [HttpPost]
    public async Task<IActionResult> AddCurrency([FromBody] CreateCurrencyRequest currencyRequest)
    {
        var createdCurrency = await _currencyRepo.AddCurrency(currencyRequest.ToCurrency());
        return CreatedAtRoute(
            GetCurrencyByIdName,
            new { id = createdCurrency.Id },
            createdCurrency
        );
    }

    [HttpGet("{id}", Name = GetCurrencyByIdName)]
    public async Task<IActionResult> GetCurrencyById([FromRoute] uint id)
    {
        var currency = await _currencyRepo.GetCurrencyById(id);
        if (currency == null)
        {
            return NotFound();
        }
        return Ok(currency);
    }

    public async Task<IActionResult> GetCurrencies(
        [FromQuery(Name = "search_query")] string searchQuery = "",
        [FromQuery(Name = "page")] int pageNumber = 1, 
        [FromQuery(Name = "page_size")] int pageSize = 10
    )
    {
        var currencies = await _currencyRepo.GetCurrencies(searchQuery, pageNumber, pageSize);
        return Ok(currencies);
    }
}
