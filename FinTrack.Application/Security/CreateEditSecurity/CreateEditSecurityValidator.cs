using CheckDigits.Net;
using FinTrack.Application.Utils;
using FinTrack.Domain.Interfaces;
using FinTrack.Resources;
using FluentValidation;

namespace FinTrack.Application.Security.CreateEditSecurity;

public sealed class CreateEditSecurityValidator: HasOwnerIdValidator<CreateEditSecurityRequest> 
{
    public CreateEditSecurityValidator(
        ICountryRepository countryRepo,
        ICurrencyRepository currencyRepo,
        ISecurityRepository securityRepo
    ) {
        // Rules for Name
        RuleFor(s => s.Name)
            .NotEmpty()
            .WithMessage(_ => GeneralMessages.EmptyNameError);

        // Rules for ISIN
        RuleFor(s => s.Isin)
            .Must((request) =>
            {
                var isinAlgorithm = Algorithms.Isin;
                return isinAlgorithm.Validate(request);
            }).WithMessage(_ => SecurityMessages.IsinValueError);
        
        RuleFor(s => s.Isin)
            .MustAsync(async (request, isin, _) =>
            {
                return !await securityRepo
                    .Exists(s => s.Isin == isin && s.OwnerId == request.OwnerId);
            }).WithMessage(_ => SecurityMessages.DuplicateIsinError);

        // Rules for NativeCurrency
        RuleFor(s => s.NativeCurrency)
            .NotEmpty()
            .WithMessage(_ => GeneralMessages.EmptyCurrencyError);
        
        RuleFor(s => s.NativeCurrency)
            .MustAsync(async (request, cancellation) =>
            {
                return await currencyRepo.Exists(c => c.Id == request);
            }).WithMessage(_ => GeneralMessages.InvalidCurrencyError);
        
        // Rules for CounterpartyCountry
        RuleFor(s => s.CounterpartyCountry)
            .MustAsync(async (request, cancellation) =>
            {
                return await countryRepo.Exists(c => c.Id == request);
            })
            .When(s => s.CounterpartyCountry != null)
            .WithMessage(GeneralMessages.InvalidCountryError);

        // Rules for source country
        RuleFor(s => s.SourceCountry)
            .NotEmpty()
            .WithMessage(_ => GeneralMessages.EmptyCountryError);
        
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
            .When(s => s.SourceCountry == null);

        RuleFor(s => s.IssuingNIF)
            .Empty()
            .Unless(s => s.SourceCountry == null);

        // Rules for operations
        RuleFor(s => s.Operations)
            .Must(OperationOrderValidator.ValidateOperations)
            .WithMessage(_ => SecurityMessages.InvalidOperationOrderError);
    }
}
