using System.Text.Json.Serialization;

namespace FinTrack.Domain.Entities;

public class Security
{
    public ulong Id { get; init; }
    public required string Name { get; set; } = null!;
    public required string Isin { get; set; } = null!;
    public required Currency NativeCurrency { get; set; } = null!;
    public Country? CounterpartyCountry { get; set; }
    public Country? SourceCountry { get; set; }
    public string? IssuingNIF { get; set; }
    public required string OwnerId { get; init; } = null!;
    public ICollection<Operation> Operations { get; init; } = new List<Operation>();
}
