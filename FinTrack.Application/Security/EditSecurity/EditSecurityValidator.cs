using FinTrack.Application.Security.EntitiesBase;
using FinTrack.Domain.Interfaces;
using FinTrack.Resources;

namespace FinTrack.Application.Security.EditSecurity;

public class EditSecurityValidator(
    ICountryRepository countryRepo,
    ICurrencyRepository currencyRepo,
    ISecurityRepository securityRepo)
    : SecurityDetailsValidator<EditSecurityRequest>(countryRepo, currencyRepo, securityRepo);