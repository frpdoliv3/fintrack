using FinTrack.Application.Utils;
using FluentValidation;

namespace FinTrack.Application.Operation.CreateOperation;

public class CreateOperationValidator: ValidatorBase<CreateOperationRequest>
{
    public CreateOperationValidator()
    {
        RuleFor(o => o.Quantity)
            .GreaterThan(0);

        RuleFor(o => o.Value)
            .GreaterThan(0);

        RuleFor(o => o.ForeignTaxes)
            .GreaterThanOrEqualTo(0);

        RuleFor(o => o.ExpensesAndCharges)
            .GreaterThanOrEqualTo(0);
    }
}
