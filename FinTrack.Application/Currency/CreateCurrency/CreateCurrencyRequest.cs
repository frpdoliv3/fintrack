using Entities = FinTrack.Domain.Entities;

namespace FinTrack.Application.Currency.CreateCurrency;

public record CreateCurrencyRequest
{
    public string Name { get; init; } = null!;
    public string Alpha3Code { get; init; } = null!;
    public string? Symbol { get; init; }
    public int? Decimals { get; init; }
    public int? NumberToMajor { get; init; }

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
