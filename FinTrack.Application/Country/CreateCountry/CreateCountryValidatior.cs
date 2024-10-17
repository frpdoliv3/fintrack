using FluentValidation;
using System.Collections.Specialized;

namespace FinTrack.Application.Country.CreateCountry
{
    public sealed class CreateCountryValidatior : AbstractValidator<CreateCountryRequest>
    {
        public CreateCountryValidatior(CountryService countryService)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MustAsync(async (request, cancellation) =>
                {
                    return !await countryService.ExistsName(request);
                })
                .WithMessage("Country with same Name already exists"); ;
            RuleFor(x => x.Alpha3Code)
                .Length(3)
                .MustAsync(async(request, cancellation) =>
                {
                    return !await countryService.ExistsAlpha3Code(request);
                })
                .WithMessage("Country with same Alpha 3 Code already exists");
            RuleFor(x => x.Alpha2Code)
                .Length(2)
                .MustAsync(async(request, cancellation) =>
                {
                    return !await countryService.ExistsAlpha2Code(request);
                })
                .WithMessage("Country with same Alpha 2 Code already exists");
            RuleFor(x => x.NumericCode)
                .GreaterThan(0)
                .LessThan(1000)
                .MustAsync(async(request, cancellation) =>
                {
                    return !await countryService.ExistsNumericCode(request);
                })
                .WithMessage("Country with same Numeric Code already exists");
        }
    }
}
