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
        // Rules for ISIN
        RuleFor(s => s.Isin)
            .Must((request) =>
            {
                var isinAlgorithm = Algorithms.Isin;
                return isinAlgorithm.Validate(request!);
            })
            .When(s => s.Isin != null)
            .WithMessage(_ => SecurityMessages.IsinValueError);

        RuleFor(s => s.Isin)
            .MustAsync(async (request, isin, _) =>
            {
                return !await securityRepo
                    .Exists(s => s.Isin == isin && s.OwnerId == request.OwnerId);
            })
            .When(s => s.Isin != null)
            .WithMessage(_ => SecurityMessages.DuplicateIsinError);

        // Rules for NativeCurrency
        RuleFor(s => s.NativeCurrency)
            .MustAsync(async (request, _) =>
            {
                return await currencyRepo.Exists(c => c.Id == request);
            })
            .When(s => s.NativeCurrency != default)
            .WithMessage(_ => GeneralMessages.InvalidCurrencyError);

        // Rules for CounterpartyCountry
        RuleFor(s => s.CounterpartyCountry)
            .MustAsync(async (request, _) =>
            {
                return await countryRepo.Exists(c => c.Id == request);
            })
            .When(s => s.CounterpartyCountry != default)
            .WithMessage(_ => GeneralMessages.InvalidCountryError);

        // Rules for source country
        RuleFor(s => s.SourceCountry)
            .MustAsync(async (request, _) =>
            {
                return await countryRepo.Exists(c => c.Id == request);
            })
            .When(s => s.IssuingNIF == null)
            .WithMessage(_ => GeneralMessages.InvalidCountryError);

        //Inter-property rules
        RuleFor(s => s.SourceCountry)
            .Empty()
            .Unless(s => s.IssuingNIF == null);

        RuleFor(s => s.IssuingNIF)
            .NotEmpty()
            .Must(IsValidNIF.Validate!)
            .When(s => s.SourceCountry == default);

        RuleFor(s => s.IssuingNIF)
            .Empty()
            .Unless(s => s.SourceCountry == default);
    }
}
