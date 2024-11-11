using System.Text.Json.Serialization;
using FinTrack.Application.Security.EntitiesBase;

namespace FinTrack.Application.Security.EditSecurity;

public class EditSecurityRequest: ISecurityDetails
{
    [JsonIgnore]
    public ulong Id { get; set; }
    
    public string? Name { get; init; }
    public string? Isin { get; init; }
    public uint NativeCurrency { get; init; }
    public uint CounterpartyCountry { get; init; } 
    public uint SourceCountry { get; init; }
    public string? IssuingNIF { get; init; }
    
    [JsonIgnore]
    public string OwnerId { get; set; } = null!; 
}
