using FinTrack.Application.Security.EntitiesBase;
using FinTrack.Domain.Interfaces;
using FinTrack.Resources;
using FluentValidation;

namespace FinTrack.Application.Security.CreateSecurity;

public sealed class CreateSecurityValidator: SecurityDetailsValidator<CreateSecurityRequest>
{
    public CreateSecurityValidator(
        ICountryRepository countryRepo,
        ICurrencyRepository currencyRepo,
        ISecurityRepository securityRepo
    ): base(countryRepo, currencyRepo, securityRepo) {
        // Rules for Name
        RuleFor(s => s.Name)
            .NotEmpty()
            .WithMessage(_ => GeneralMessages.EmptyNameError);
        
        // Rules for ISIN
        RuleFor(s => s.Isin)
            .NotEmpty()
            .WithMessage(_ => SecurityMessages.IsinValueError);
        
        // Rules for NativeCurrency
        RuleFor(s => s.NativeCurrency)
            .NotEmpty()
            .WithMessage(_ => GeneralMessages.EmptyCurrencyError);
        
        // Rules for source country
        RuleFor(s => s.SourceCountry)
            .NotEmpty()
            .WithMessage(_ => GeneralMessages.EmptyCountryError);
        
        // Rules for operations
        RuleFor(s => s.Operations)
            .Must(OperationOrderValidator.ValidateOperations)
            .WithMessage(_ => SecurityMessages.InvalidOperationOrderError);
    }
}
