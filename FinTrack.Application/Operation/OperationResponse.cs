using System.Text.Json.Serialization;
using FinTrack.Domain.Entities;

namespace FinTrack.Application.Operation;

public record OperationResponse(
    ulong Id,
    OperationType OperationType,
    DateOnly OperationDate,
    decimal Value,
    uint Quantity,
    decimal ForeignTaxes,
    decimal ExpensesAndCharges
);
