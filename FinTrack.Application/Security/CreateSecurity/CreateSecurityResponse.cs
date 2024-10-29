using FinTrack.Application.Operation;
using Entities = FinTrack.Domain.Entities;

namespace FinTrack.Application.Security.CreateSecurity;

public record CreateSecurityResponse(
    ulong Id, 
    string Name,
    string Isin,
    Entities.Currency NativeCurrency,
    List<OperationResponse> Operations,
    Entities.Country? CounterpartyCountry,
    Entities.Country? SourceCountry,
    string? IssuingNIF
);
