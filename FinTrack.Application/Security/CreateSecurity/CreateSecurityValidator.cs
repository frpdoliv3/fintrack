using CheckDigits.Net;
using FinTrack.Application.Operation;
using FinTrack.Domain.Entities;
using FinTrack.Domain.Interfaces;
using FluentValidation;

namespace FinTrack.Application.Security.CreateSecurity;

public sealed class CreateSecurityValidator: ValidatorBase<CreateSecurityRequest> 
{
    public CreateSecurityValidator(
        ICountryRepository countryRepo,
        ICurrencyRepository currencyRepo
    ) {
        RuleFor(s => s.Name)
            .NotEmpty();

        RuleFor(s => s.Isin)
            .NotEmpty()
            .Must((request) =>
            {
                var isinAlgorithm = Algorithms.Isin;
                return isinAlgorithm.Validate(request);
            }).WithMessage("Invalid ISIN");

        RuleFor(s => s.NativeCurrency)
            .NotEmpty()
            .MustAsync(async (request, cancellation) =>
            {
                return await currencyRepo.Exists(c => c.Id == request);
            }).WithMessage("Currency does not exist");

        RuleFor(s => s.CounterpartyCountry)
            .MustAsync(async (request, cancellation) =>
            {
                return await countryRepo.Exists(c => c.Id == request);
            })
            .When(s => s.CounterpartyCountry != null)
            .WithMessage("Country does not exist");

        RuleFor(s => s.SourceCountry)
            .NotEmpty()
            .MustAsync(async (request, cancellation) =>
            {
                return await countryRepo.Exists(c => c.Id == request);
            })
            .When(s => s.IssuingNIF == null);
            
        RuleFor(s => s.SourceCountry)
            .Empty()
            .Unless(s => s.IssuingNIF == null);

        RuleFor(s => s.IssuingNIF)
            .NotEmpty()
            .When(s => s.SourceCountry == null);

        RuleFor(s => s.IssuingNIF)
            .Empty()
            .Unless(s => s.SourceCountry == null);

        RuleFor(s => s.Operations)
            .Must(ValidateOperations);
    }

    public static bool ValidateOperations(List<CreateOperationRequest> operations)
    {
        operations.Sort((a, b) =>
        {
            return a.OperationDate.CompareTo(b.OperationDate);
        });
        decimal curQuantity = 0;
        foreach (CreateOperationRequest operation in operations)
        {
            if (operation.OperationType == OperationType.Sell)
            {
                curQuantity -= operation.Quantity;
            }
            if (operation.OperationType == OperationType.Buy)
            {
                curQuantity += operation.Quantity;
            }
            if (curQuantity < 0)
            {
                return false;
            }
        }
        return true;
    }
}
