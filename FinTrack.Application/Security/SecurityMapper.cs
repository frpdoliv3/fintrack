using FinTrack.Application.Operation.CreateOperation;
using FinTrack.Application.Security.CreateEditSecurity;
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

    public async Task<Entities.Security> ToSecurity(CreateEditSecurityRequest createEditSecurityRequest)
    {
        var nativeCurrency = await _currencyRepo
            .GetCurrencyById(createEditSecurityRequest.NativeCurrency);
        
        var counterpartyCountry = createEditSecurityRequest.CounterpartyCountry != null ? 
            await _countryRepo.GetCountryById(createEditSecurityRequest.CounterpartyCountry.Value) : 
            null;
        
        var sourceCountry = createEditSecurityRequest.SourceCountry != null ? 
            await _countryRepo.GetCountryById(createEditSecurityRequest.SourceCountry.Value) : 
            null;

        return new Entities.Security
        {
            Name = createEditSecurityRequest.Name,
            Isin = createEditSecurityRequest.Isin,
            NativeCurrency = nativeCurrency!,
            CounterpartyCountry = counterpartyCountry,
            SourceCountry = sourceCountry,
            IssuingNIF = createEditSecurityRequest.IssuingNIF,
            OwnerId = createEditSecurityRequest.OwnerId!,
            Operations = createEditSecurityRequest.Operations
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
}