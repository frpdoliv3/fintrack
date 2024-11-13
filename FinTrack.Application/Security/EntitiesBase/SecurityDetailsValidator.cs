using CheckDigits.Net;
using FinTrack.Application.Utils;
using FinTrack.Domain.Interfaces;
using FinTrack.Resources;
using FluentValidation;

namespace FinTrack.Application.Security.EntitiesBase;

public class SecurityDetailsValidator<T>: ValidatorBase<T> where T : ISecurityDetails
{
    protected SecurityDetailsValidator(
        ICountryRepository countryRepo,
        ICurrencyRepository currencyRepo,
        ISecurityRepository securityRepo
    )
    {
        // Rules for Name
        RuleFor(s => s.Name)
            .NotEmpty()
            .WithMessage(_ => GeneralMessages.EmptyNameError);
        
        // Rules for ISIN
        RuleFor(s => s.Isin)
            .NotEmpty()
            .WithMessage(_ => SecurityMessages.IsinValueError);
        
        RuleFor(s => s.Isin)
            .Must((request) =>
            {
                var isinAlgorithm = Algorithms.Isin;
                return isinAlgorithm.Validate(request!);
            })
            .When(s => s.Isin != null)
            .WithMessage(_ => SecurityMessages.IsinValueError);

        RuleFor(s => s.Isin)
            .Must((request, isin) =>
            {
                return !securityRepo
                    .Exists(s => s.Isin == isin && s.OwnerId == request.OwnerId);
            })
            .When(s => s.Isin != null)
            .WithMessage(_ => SecurityMessages.DuplicateIsinError);

        // Rules for NativeCurrency
        RuleFor(s => s.NativeCurrency)
            .NotEmpty()
            .WithMessage(_ => GeneralMessages.EmptyCurrencyError);
        
        RuleFor(s => s.NativeCurrency)
            .Must(nativeCurrencyId =>
            {
                return currencyRepo.Exists(c => c.Id == nativeCurrencyId);
            })
            .When(s => s.NativeCurrency != default)
            .WithMessage(_ => GeneralMessages.InvalidCurrencyError);

        // Rules for CounterpartyCountry
        RuleFor(s => s.CounterpartyCountry)
            .Must(counterpartyCountryId =>
            {
                return countryRepo.Exists(c => c.Id == counterpartyCountryId);
            })
            .When(s => s.CounterpartyCountry != default)
            .WithMessage(_ => GeneralMessages.InvalidCountryError);

        // Rules for source country
        RuleFor(s => s.SourceCountry)
            .NotEmpty()
            .WithMessage(_ => GeneralMessages.EmptyCountryError);
        
        RuleFor(s => s.SourceCountry)
            .Must(sourceCountryId =>
            {
                return countryRepo.Exists(c => c.Id == sourceCountryId);
            })
            .When(s => s.SourceCountry != default)
            .WithMessage(_ => GeneralMessages.InvalidCountryError);
        
        //Inter-property rules
        RuleFor(s => s.SourceCountry)
            .Empty()
            .Unless(s => s.IssuingNIF == null);

        RuleFor(s => s.SourceCountry)
            .NotEmpty()
            .When(s => s.IssuingNIF == null);

        RuleFor(s => s.IssuingNIF)
            .Must(IsValidNIF.Validate!)
            .When(s => s.IssuingNIF != null);

        RuleFor(s => s.IssuingNIF)
            .Empty()
            .Unless(s => s.SourceCountry == default);
        
        RuleFor(s => s.IssuingNIF)
            .NotEmpty()
            .When(s => s.SourceCountry == default);
    }
}
