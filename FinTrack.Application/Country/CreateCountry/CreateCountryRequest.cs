using Entities = FinTrack.Domain.Entities;

namespace FinTrack.Application.Country.CreateCountry
{
    public record CreateCountryRequest
    {
        public string Name { get; init; } = string.Empty;
        public string Alpha2Code { get; init; } = string.Empty;
        public string Alpha3Code { get; init; } = string.Empty;
        public int NumericCode { get; init; }

        public Entities.Country ToCountry()
        {
            return new Entities.Country
            {
                Name = Name,
                Alpha2Code = Alpha2Code,
                Alpha3Code = Alpha3Code,
                NumericCode = NumericCode,
            };
        }
    }
}
