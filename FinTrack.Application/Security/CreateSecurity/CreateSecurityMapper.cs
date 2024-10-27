using FinTrack.Domain.Interfaces;
using Entities = FinTrack.Domain.Entities; 

namespace FinTrack.Application.Security.CreateSecurity;

public class CreateSecurityMapper
{
    private readonly ICountryRepository _countryRepo;
    private readonly ICurrencyRepository _currencyRepo;
    
    public CreateSecurityMapper(ICountryRepository countryRepo, ICurrencyRepository currencyRepo)
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
            Operations = createSecurityRequest.Operations
                .Select(o => o.ToOperation())
                .ToList()
        };
    }
}