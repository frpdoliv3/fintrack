using Entities = FinTrack.Domain.Entities;

namespace FinTrack.Application.Operation.CreateOperation;

public static class OperationResponseMapper
{
    public static OperationResponse ToOperationResponse(this Entities.Operation operation)
    {
        return new OperationResponse(
            Id: operation.Id,
            OperationType: operation.OperationType,
            OperationDate: operation.OperationDate,
            Value: operation.Value,
            Quantity: operation.Quantity,
            ExpensesAndCharges: operation.ExpensesAndCharges,
            ForeignTaxes: operation.ForeignTaxes
        );
    }
}