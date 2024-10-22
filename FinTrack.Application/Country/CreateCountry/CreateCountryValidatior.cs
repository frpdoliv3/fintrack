using FinTrack.Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace FinTrack.Application.Country.CreateCountry;

/*
 * Making this class internal breaks the reflection necessary to inject valitators into the request pipeline
 */
public sealed class CreateCountryValidatior : ValidatorBase<CreateCountryRequest>
{
    public CreateCountryValidatior(ICountryRepository countryRepository): base()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MustAsync(async (request, cancellation) =>
            {
                return !await countryRepository.Exists(x => x.Name == request);
            })
            .WithMessage("Country with same Name already exists"); ;
        RuleFor(x => x.Alpha3Code)
            .Length(3)
            .MustAsync(async(request, cancellation) =>
            {
                return !await countryRepository.Exists(x => x.Alpha3Code == request);
            })
            .WithMessage("Country with same Alpha 3 Code already exists");
        RuleFor(x => x.Alpha2Code)
            .Length(2)
            .MustAsync(async(request, cancellation) =>
            {
                return !await countryRepository.Exists(x => x.Alpha2Code == request);
            })
            .WithMessage("Country with same Alpha 2 Code already exists");
    }
}
