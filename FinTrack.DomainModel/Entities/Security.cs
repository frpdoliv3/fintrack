namespace FinTrack.Domain.Entities;

public class Security
{
    public long Id { get; init; }
    public string Name { get; init; } = null!;
    public string Isin { get; init; } = null!;
    public Currency NativeCurrency { get; init; } = null!;
    public Country? CounterpartyCountry { get; init; }
    public Country? SourceCountry { get; init; }
    public string? IssuingNIF { get; init; }
    public ICollection<Operation> Operations { get; init; } = new List<Operation>();
}
