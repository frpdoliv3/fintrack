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
    private readonly ICurrencyRepository _currencyRepository;

    private const string GetCurrencyByIdName = "GetCurrencyById";

    public CurrenciesController(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    [HttpPost]
    public async Task<IActionResult> AddCurrency([FromBody] CreateCurrencyRequest currencyRequest)
    {
        var createdCurrency = await _currencyRepository.AddCurrency(currencyRequest.ToCurrency());
        return CreatedAtRoute(
            GetCurrencyByIdName,
            new { id = createdCurrency.Id },
            createdCurrency
        );
    }

    [HttpGet("{id}", Name = GetCurrencyByIdName)]
    public async Task<IActionResult> GetCurrencyById([FromRoute] uint id)
    {
        var currency = await _currencyRepository.GetCurrencyById(id);
        if (currency == null)
        {
            return NotFound();
        }
        return Ok(currency);
    }
}
