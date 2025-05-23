﻿using System.Text.Json.Serialization;

namespace FinTrack.Domain.Entities;

public enum OperationType { Sell, Buy };

public class Operation
{
    public uint Id { get; set; }
    public required OperationType OperationType { get; set; }
    public required DateOnly OperationDate {  get; set; }
    public required decimal Value { get; set; }
    public required uint Quantity { get; set; } = 1;
    public decimal ForeignTaxes { get; set; }
    public decimal ExpensesAndCharges { get; set; }
    public Security Security { get; set; } = null!;
}
