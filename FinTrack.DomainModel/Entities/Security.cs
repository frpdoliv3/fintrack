namespace FinTrack.Domain.Entities;

public class Security
{
    public ulong Id { get; init; }
    public required string Name { get; init; } = null!;
    public required string Isin { get; init; } = null!;
    public required Currency NativeCurrency { get; init; } = null!;
    public Country? CounterpartyCountry { get; init; }
    public Country? SourceCountry { get; init; }
    public string? IssuingNIF { get; init; }
    public required string OwnerId { get; init; } = null!;
    public ICollection<Operation> Operations { get; init; } = new List<Operation>();
}
