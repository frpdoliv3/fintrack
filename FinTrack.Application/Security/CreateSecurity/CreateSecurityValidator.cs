using CheckDigits.Net;
using FinTrack.Application.Operation;
using FinTrack.Application.Operation.CreateOperation;
using FinTrack.Application.Utils;
using FinTrack.Domain.Entities;
using FinTrack.Domain.Interfaces;
using FluentValidation;

namespace FinTrack.Application.Security.CreateSecurity;

public sealed class CreateSecurityValidator: HasOwnerIdValidator<CreateSecurityRequest> 
{
    public CreateSecurityValidator(
        ICountryRepository countryRepo,
        ICurrencyRepository currencyRepo,
        ISecurityRepository securityRepo
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
        
        RuleFor(s => s.Isin)
            .MustAsync(async (request, isin, _) =>
            {
                return !await securityRepo
                    .Exists(s => s.Isin == isin && s.OwnerId == request.OwnerId);
            });
        
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
            .Must(IsValidNIF.Validate!)
            .When(s => s.SourceCountry == null);

        RuleFor(s => s.IssuingNIF)
            .Empty()
            .Unless(s => s.SourceCountry == null);

        RuleFor(s => s.Operations)
            .Must(ValidateOperations);
    }

    private static bool ValidateOperations(List<CreateOperationRequest> operations)
    {
        operations.Sort((a, b) => 
            a.OperationDate.CompareTo(b.OperationDate));
        decimal curQuantity = 0;
        foreach (CreateOperationRequest operation in operations)
        {
            switch (operation.OperationType)
            {
                case OperationType.Sell:
                    curQuantity -= operation.Quantity;
                    break;
                case OperationType.Buy:
                    curQuantity += operation.Quantity;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (curQuantity < 0)
            {
                return false;
            }
        }
        return true;
    }
}
