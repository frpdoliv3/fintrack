using FluentValidation;
using System.Collections.Specialized;

namespace FinTrack.Application.Country.CreateCountry
{
    internal sealed class CreateCountryValidatior : AbstractValidator<CreateCountryRequest>
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
        }
    }
}
