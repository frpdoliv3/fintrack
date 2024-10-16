using FinTrack.Domain.Entities;

namespace FinTrack.Application.UseCases.CreateCountry
{
    public record CreateCountryRequest
    {
        public string Name { get; init;  } = string.Empty;
        public string Alpha2Code { get; init; } = string.Empty;
        public string Alpha3Code { get; init; } = string.Empty;
        public int NumericCode { get; init; }

        public Country ToCountry()
        {
            return new Country
            {
                Name = Name,
                Alpha2Code = Alpha2Code,
                Alpha3Code = Alpha3Code,
                NumericCode = NumericCode,
            };
        }
    }
}
