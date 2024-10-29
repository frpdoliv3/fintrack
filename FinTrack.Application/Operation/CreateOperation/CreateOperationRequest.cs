using FinTrack.Domain.Entities;
using Entities = FinTrack.Domain.Entities;

namespace FinTrack.Application.Operation.CreateOperation;

public record CreateOperationRequest(
    OperationType OperationType,
    DateOnly OperationDate,
    decimal Value,
    int Quantity,
    decimal ForeignTaxes,
    decimal ExpensesAndCharges
) {
    public Entities.Operation ToOperation()
    {
        return new Entities.Operation
        {
            OperationType = OperationType,
            OperationDate = OperationDate,
            Value = Value,
            Quantiy = (uint) Quantity,
            ForeignTaxes = ForeignTaxes,
            ExpensesAndCharges = ExpensesAndCharges
        };
    }
}
