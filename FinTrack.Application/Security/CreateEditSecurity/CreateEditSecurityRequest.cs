using FinTrack.Application.Operation.CreateOperation;
using FinTrack.Application.Utils;

namespace FinTrack.Application.Security.CreateEditSecurity;

public record CreateEditSecurityRequest(
    string Name,
    string Isin,
    uint NativeCurrency,
    List<CreateOperationRequest> Operations,
    uint? CounterpartyCountry,
    uint? SourceCountry,
    string? IssuingNIF
) : IHasOwnerId
{
    public string? OwnerId { get; set; }
}
