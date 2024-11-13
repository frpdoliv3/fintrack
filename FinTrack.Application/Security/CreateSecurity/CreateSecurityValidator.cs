using FinTrack.Application.Security.EntitiesBase;
using FinTrack.Application.Utils;
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
        // Rules for operations
        RuleFor(s => s.Operations)
            .Must(OperationOrderValidator.ValidateOperations)
            .WithMessage(_ => SecurityMessages.InvalidOperationOrderError);
    }
}
