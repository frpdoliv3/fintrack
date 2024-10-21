using FinTrack.Application.Currency.CreateCurrency;
using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyController(ICurrencyRepository currencyRepository) 
        {
            _currencyRepository = currencyRepository;
        }

        [HttpPost]
        public async Task AddCurrency([FromBody] CreateCurrencyRequest currencyRequest) 
        {
            await _currencyRepository.AddCurrency(currencyRequest.ToCurrency());
        }
    }
}
