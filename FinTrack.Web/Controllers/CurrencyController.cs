using FinTrack.Application.Currency.CreateCurrency;
using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Admin")]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyRepository _currencyRepository;

    private const string GET_CURRENCY_BY_ID_NAME = "GetCurrencyById";

    public CurrencyController(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    [HttpPost]
    public async Task<IResult> AddCurrency([FromBody] CreateCurrencyRequest currencyRequest)
    {
        var createdCurrency = await _currencyRepository.AddCurrency(currencyRequest.ToCurrency());
        return Results.CreatedAtRoute(
            GET_CURRENCY_BY_ID_NAME,
            new { id = createdCurrency.Id },
            createdCurrency
        );
    }

    [HttpGet("{id}", Name = GET_CURRENCY_BY_ID_NAME)]
    public async Task<IResult> GetCurrencyById([FromRoute] uint id)
    {
        var currency = await _currencyRepository.FindCurrencyById(id);
        if (currency == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(currency);
    }
}
