using FinTrack.Domain.Entities;

namespace FinTrack.Application.Operation;

public class CreateOperationRequest
{
    public required OperationType OperationType { get; set; }
    public required DateOnly OperationDate { get; set; }
    public required decimal Value { get; set; }
    public required int Quantity { get; set; }
    public decimal ForeignTaxes { get; set; } = decimal.Zero;
    public decimal ExpensesAndCharges { get; set; } = decimal.Zero;
}
