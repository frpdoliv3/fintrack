namespace FinTrack.Application.Security.EntitiesBase;

public interface ISecurityDetails : IHasOwnerId
{
    public string Name { get; }
    public string Isin { get; }
    public uint NativeCurrency { get; }
    public uint CounterpartyCountry { get; }
    public uint SourceCountry { get; }
    public string? IssuingNIF { get; }
}
