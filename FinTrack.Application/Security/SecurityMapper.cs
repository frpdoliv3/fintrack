using FinTrack.Application.Operation.CreateOperation;
using FinTrack.Application.Security.EditSecurity;
using FinTrack.Application.Security.GetSecurity;
using FinTrack.Application.Security.GetSecurityStatus;
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
        
        var counterpartyCountry = createSecurityRequest.CounterpartyCountry != default ? 
            await _countryRepo.GetCountryById(createSecurityRequest.CounterpartyCountry) : 
            null;
        
        var sourceCountry = createSecurityRequest.SourceCountry != default ? 
            await _countryRepo.GetCountryById(createSecurityRequest.SourceCountry) : 
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

    public GetSecurityResponse? ToGetSecurityResponse(Entities.Security? security)
    {
        if (security == null) { return null; }
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

    public async Task<Entities.Security> UpdateSecurity(EditSecurityRequest updateRequest, Entities.Security security)
    {
        security.Name = updateRequest.Name;
        security.Isin = updateRequest.Isin;

        var shouldUpdateNativeCurrency = updateRequest.NativeCurrency != security.NativeCurrency.Id;
        if (shouldUpdateNativeCurrency)
        {
            var nativeCurrency = await _currencyRepo.GetCurrencyById(updateRequest.NativeCurrency);
            security.NativeCurrency = nativeCurrency!;
        }

        if (
            updateRequest.CounterpartyCountry != default && 
            updateRequest.CounterpartyCountry != security.CounterpartyCountry?.Id
        ) {
            var counterpartyCountry = await _countryRepo.GetCountryById(updateRequest.CounterpartyCountry);
            security.CounterpartyCountry = counterpartyCountry;
        }
        if (updateRequest.CounterpartyCountry == default)
        {
            security.CounterpartyCountry = null;
        }
        
        if (updateRequest.SourceCountry != default && updateRequest.SourceCountry != security.SourceCountry?.Id) {
            var sourceCountry = await _countryRepo.GetCountryById(updateRequest.SourceCountry);
            security.SourceCountry = sourceCountry;
        }
        if (updateRequest.SourceCountry == default)
        {
            security.SourceCountry = null;
        }
        
        security.IssuingNIF = updateRequest.IssuingNIF;
        return security;
    }
}