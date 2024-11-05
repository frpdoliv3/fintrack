using FinTrack.Application.Utils;
using FinTrack.Resources;
using FluentValidation;

namespace FinTrack.Application.Operation.CreateOperation;

public class CreateOperationValidator: ValidatorBase<CreateOperationRequest>
{
    public CreateOperationValidator()
    {
        RuleFor(o => o.Quantity)
            .GreaterThan(0)
            .WithMessage(_ => OperationMessages.QuantityValueError);

        RuleFor(o => o.Value)
            .GreaterThan(0)
            .WithMessage(_ => OperationMessages.ValueValueError);

        RuleFor(o => o.ForeignTaxes)
            .GreaterThanOrEqualTo(0)
            .WithMessage(_ => OperationMessages.ForeignTaxesValueError);

        RuleFor(o => o.ExpensesAndCharges)
            .GreaterThanOrEqualTo(0)
            .WithMessage(_ => OperationMessages.ExpensesAndChargesValueError);
    }
}
