namespace FinTrack.Domain.Entities;

public class Security
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Isin { get; set; } = null!;
    public Currency NativeCurrency { get; set; } = null!;
    public Country? CounterpartyCountry { get; set; }
    public Country? SourceCountry { get; set; }
    public string? IssuingNIF { get; set; }
    public ICollection<Operation> Operations { get; } = new List<Operation>();
}
