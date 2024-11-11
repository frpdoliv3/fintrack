using System.Text.Json.Serialization;
using FinTrack.Application.Operation.CreateOperation;

namespace FinTrack.Application.Security.CreateSecurity;

public class CreateSecurityRequest: EntitiesBase.ISecurityDetails
{
    public string Name { get; init; } = null!;
    public string Isin { get; init; } = null!;
    public uint NativeCurrency { get; init; }
    public uint CounterpartyCountry { get; init; } 
    public uint SourceCountry { get; init; }
    public string IssuingNIF { get; init; } = null!;
    
    [JsonIgnore]
    public string OwnerId { get; set; } = null!; 

    public List<CreateOperationRequest> Operations { get; init; } = new();
}
