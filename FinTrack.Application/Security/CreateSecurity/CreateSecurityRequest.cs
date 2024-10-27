using FinTrack.Application.Operation;

namespace FinTrack.Application.Security.CreateSecurity;

public record CreateSecurityRequest
{
    public string Name { get; init; } = null!;
    public string Isin { get; init; } = null!;
    public uint NativeCurrency { get; init; }
    public uint? CounterpartyCountry { get; init; }
    public uint? SourceCountry { get; init; }
    public string? IssuingNIF { get; init; }
    public List<CreateOperationRequest> Operations { get; init; } = new();
}
