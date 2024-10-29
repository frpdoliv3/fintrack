using Entities = FinTrack.Domain.Entities;

namespace FinTrack.Application.Country.CreateCountry;

public record CreateCountryRequest(
    string Name,
    string Alpha2Code,
    string Alpha3Code
) { 
    public Entities.Country ToCountry()
    {
        return new Entities.Country
        {
            Name = Name,
            Alpha2Code = Alpha2Code,
            Alpha3Code = Alpha3Code
        };
    }
}
