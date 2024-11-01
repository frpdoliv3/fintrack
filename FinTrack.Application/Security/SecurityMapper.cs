using FinTrack.Application.Operation.CreateOperation;
using FinTrack.Application.Security.GetSecurity;
using FinTrack.Domain.Interfaces;
using Entities = FinTrack.Domain.Entities; 

namespace FinTrack.Application.Security.CreateSecurity;

public class SecurityMapper
{
    private readonly ICountryRepository _countryRepo;
    private readonly ICurrencyRepository _currencyRepo;
    
    public SecurityMapper(ICountryRepository countryRepo, ICurrencyRepository currencyRepo)
    {
        _currencyRepo = currencyRepo;
        _countryRepo = countryRepo;
    }

    public async Task<Entities.Security> ToSecurity(CreateSecurityRequest createSecurityRequest)
    {
        var nativeCurrency = await _currencyRepo
            .GetCurrencyById(createSecurityRequest.NativeCurrency);
        
        var counterpartyCountry = createSecurityRequest.CounterpartyCountry != null ? 
            await _countryRepo.GetCountryById(createSecurityRequest.CounterpartyCountry.Value) : 
            null;
        
        var sourceCountry = createSecurityRequest.SourceCountry != null ? 
            await _countryRepo.GetCountryById(createSecurityRequest.SourceCountry.Value) : 
            null;

        return new Entities.Security
        {
            Name = createSecurityRequest.Name,
            Isin = createSecurityRequest.Isin,
            NativeCurrency = nativeCurrency!,
            CounterpartyCountry = counterpartyCountry,
            SourceCountry = sourceCountry,
            IssuingNIF = createSecurityRequest.IssuingNIF,
            OwnerId = createSecurityRequest.OwnerId!,
            Operations = createSecurityRequest.Operations
                .Select(o => o.ToOperation())
                .ToList()
        };
    }

    public GetSecurityResponse ToGetSecurityResponse(Entities.Security security)
    {
        return new GetSecurityResponse(
            Id: security.Id,
            Name: security.Name,
            Isin: security.Isin,
            NativeCurrency: security.NativeCurrency,
            CounterpartyCountry: security.CounterpartyCountry,
            SourceCountry: security.SourceCountry,
            IssuingNIF: security.IssuingNIF
        );
    }
}