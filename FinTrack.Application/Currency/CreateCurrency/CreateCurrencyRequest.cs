using Entities = FinTrack.Domain.Entities;

namespace FinTrack.Application.Currency.CreateCurrency;

public record CreateCurrencyRequest(
    string Name,
    string Alpha3Code,
    string? Symbol,
    int? Decimals,
    int? NumberToMajor
) { 
    public Entities.Currency ToCurrency()
    {
        return new Entities.Currency
        {
            Name = Name,
            Alpha3Code = Alpha3Code,
            Symbol = Symbol,
            Decimals = (ushort?) Decimals ?? Entities.Currency.DEFAULT_DECIMALS,
            NumberToMajor = (ushort?) NumberToMajor ?? Entities.Currency.DEFAULT_NUMBER_TO_MAJOR
        };
    }
}
