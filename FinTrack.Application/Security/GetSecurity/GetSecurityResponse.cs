using FinTrack.Application.Operation;
using Entities = FinTrack.Domain.Entities;

namespace FinTrack.Application.Security.GetSecurity;

public record GetSecurityResponse(
    ulong Id, 
    string Name,
    string Isin,
    Entities.Currency NativeCurrency,
    Entities.Country? CounterpartyCountry,
    Entities.Country? SourceCountry,
    string? IssuingNIF
);
