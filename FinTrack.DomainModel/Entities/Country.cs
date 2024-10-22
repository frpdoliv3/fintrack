using System.ComponentModel.DataAnnotations;

namespace FinTrack.Domain.Entities;

public class Country
{
    public uint Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required string Alpha2Code { get; set; } = string.Empty;
    public required string Alpha3Code { get; set; } = string.Empty;
}
