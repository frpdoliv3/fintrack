using FinTrack.Application.Operation;

namespace FinTrack.Application.Security.CreateSecurity;

public class CreateSecurityRequest
{
    public string Name { get; set; } = null!;
    public string Isin { get; set; } = null!;
    public uint NativeCurrency { get; set; }
    public uint? CounterpartyCountry { get; set; }
    public uint? SourceCountry { get; set; }
    public string? IssuingNIF { get; set; }
    public List<CreateOperationRequest> Operations { get; set; } = new List<CreateOperationRequest>();
}
