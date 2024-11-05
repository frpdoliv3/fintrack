using FinTrack.Application.Utils;
using FinTrack.Domain.Interfaces;
using FinTrack.Resources;
using FluentValidation;

namespace FinTrack.Application.Country.CreateCountry;

/*
 * Making this class internal breaks the reflection necessary to inject validators into the request pipeline
 */
public sealed class CreateCountryValidator : ValidatorBase<CreateCountryRequest>
{
    public CreateCountryValidator(ICountryRepository countryRepository): base()
    {
        // Rules for Name
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(_ => GeneralMessages.EmptyNameError);
        
        RuleFor(x => x.Name)
            .MustAsync(async (request, cancellation) =>
            {
                return !await countryRepository.Exists(x => x.Name == request);
            })
            .WithMessage(_ => CountryMessages.DuplicateNameError); ;
        
        // Rules for Alpha3Code
        RuleFor(x => x.Alpha3Code)
            .Length(3)
            .WithMessage(_ => CountryMessages.Alpha3CodeLengthError);
            
        RuleFor(x => x.Alpha3Code)
            .MustAsync(async(request, cancellation) =>
            {
                return !await countryRepository.Exists(x => x.Alpha3Code == request);
            })
            .WithMessage(_ => CountryMessages.DuplicateAlpha3CodeError);
        
        // Rules for Alpha2Code
        RuleFor(x => x.Alpha2Code)
            .Length(2)
            .WithMessage(_ => CountryMessages.Alpha2CodeLengthError);
        
        RuleFor(x => x.Alpha2Code)
            .MustAsync(async(request, cancellation) =>
            {
                return !await countryRepository.Exists(x => x.Alpha2Code == request);
            })
            .WithMessage(_ => CountryMessages.DuplicateAlpha2CodeError);
    }
}
