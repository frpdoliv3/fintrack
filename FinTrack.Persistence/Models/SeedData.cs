using FinTrack.Domain.Entities;
using FinTrack.Domain.Interfaces;
using FinTrack.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace FinTrack.Persistence.Models;
internal class SeedData
{
    private readonly string _resourceBasePath;
    private readonly IAuthRepository _authRepo;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<EFUser> _userManager;
    private readonly ICountryRepository _countryRepo;
    private readonly FinTrackDbContext _context;
    private readonly ICurrencyRepository _currencyRepo;
    private readonly ISecurityRepository _securityRepo;
    private string _regularUserId = string.Empty;
    
    public SeedData(IServiceProvider serviceProvider, string resourceBasePath)
    {
        _resourceBasePath = resourceBasePath;
        _authRepo = serviceProvider.GetRequiredService<IAuthRepository>();
        _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        _userManager = serviceProvider.GetRequiredService<UserManager<EFUser>>();
        _countryRepo = serviceProvider.GetRequiredService<ICountryRepository>();
        _context = serviceProvider.GetRequiredService<FinTrackDbContext>();
        _currencyRepo = serviceProvider.GetRequiredService<ICurrencyRepository>();
        _securityRepo = serviceProvider.GetRequiredService<ISecurityRepository>();
    }

    public async Task Initialize()
    {
        await CreateUsers();
        await CreateCountries();
        await CreateCurrencies();
        await CreateSecurities();
    }
    
    private async Task<bool> ShouldCreateAuthentication()
    {
        if (await _authRepo.ExistsAnyUser())
        {
            return false;
        }
        foreach (var role in UserRole.GetUserRoles()) 
        {
            if (await _roleManager.RoleExistsAsync(role))
            {
                return false;
            }
        }
        return true;
    }

    private async Task CreateSecurities()
    {
        if (await _context.Securities.AnyAsync())
        {
            return;
        }

        var nativeCurrency = await _currencyRepo
            .GetCurrencies("Euro", 1, 5);
        var sourceCountry = await _countryRepo
            .GetCountries("Irlanda", 1, 5);
        var security = new Security
        {
            Name = "Vanguard FTSE All-World UCITS ETF USD Acc",
            Isin = "IE00BK5BQT80",
            NativeCurrency = nativeCurrency.Items[0],
            OwnerId = _regularUserId,
            SourceCountry = sourceCountry.Items[0],
            Operations = new List<Operation>
            {
                new() {
                    OperationType = OperationType.Buy,
                    OperationDate = DateOnly.Parse("2021-01-01"),
                    Value = 88,
                    Quantity = 2
                },
                new() {
                    OperationType = OperationType.Buy,
                    OperationDate = DateOnly.Parse("2021-02-01"),
                    Value = 90,
                    Quantity = 2
                },
                new() {
                    OperationType = OperationType.Buy,
                    OperationDate = DateOnly.Parse("2021-03-01"),
                    Value = 92,
                    Quantity = 2
                },
                new() {
                    OperationType = OperationType.Sell,
                    OperationDate = DateOnly.Parse("2021-02-20"),
                    Value = 87,
                    Quantity = 4
                },
                new() {
                    OperationType = OperationType.Sell,
                    OperationDate = DateOnly.Parse("2022-02-20"),
                    Value = 100,
                    Quantity = 2
                },
                new() {
                    OperationType = OperationType.Buy,
                    OperationDate = DateOnly.Parse("2023-02-20"),
                    Value = 120,
                    Quantity = 5
                }
            }
        };
        await _securityRepo.AddSecurity(security);
    }
    
    private async Task CreateUsers()
    {
        if (!await ShouldCreateAuthentication())
        {
            return;
        }

        var adminUser = new CreateUser
        {
            UserName = "admin",
            Email = "admin@example.com",
            Password = "Example_admin1"
        };

        await _authRepo.RegisterUser(adminUser);
        foreach (var role in UserRole.GetUserRoles())
        {
            await _roleManager.CreateAsync(new IdentityRole(role));
        }

        await _userManager.AddToRoleAsync(
            (await _userManager.FindByEmailAsync(adminUser.Email))!,
            UserRole.Admin
        );

        var regularUser = new CreateUser
        {
            UserName = "fpoliveira",
            Email = "fpoliveira@gmail.com",
            Password = "Example_user1"
        };

        var regularUserId = await _authRepo.RegisterUser(regularUser);
        _regularUserId = regularUserId;
    }

    private async Task CreateCountries()
    {
        if (await _context.Countries.AnyAsync())
        {
            return;
        }

        await using FileStream stream = File.OpenRead($"{_resourceBasePath}/countries.json");
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var countries = await JsonSerializer.DeserializeAsync<List<Country>>(stream, options);
        foreach (var country in countries!)
        {
            await _countryRepo.AddCountry(country);
        }
    }

    private async Task CreateCurrencies()
    {
        if (await _context.Currencies.AnyAsync())
        {
            return;
        }

        using FileStream stream = File.OpenRead($"{_resourceBasePath}/currencies.json");
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var currencies = await JsonSerializer.DeserializeAsync<List<CurrencyMigration>>(stream, options);
        foreach (var currency in currencies!)
        {
            await _currencyRepo.AddCurrency(currency.ToCurrency());
        }
    }

    private record CurrencyMigration
    {
        public string Name { get; init; } = null!;
        public string Alpha3Code { get; init; } = null!;
        public string? Symbol { get; init; }
        public int? Decimals { get; init; }
        public int? NumberToMajor { get; init; }

        public Currency ToCurrency()
        {
            return new Currency
            {
                Name = Name,
                Alpha3Code = Alpha3Code,
                Symbol = Symbol,
                Decimals = (ushort?)Decimals ?? Currency.DEFAULT_DECIMALS,
                NumberToMajor = (ushort?)NumberToMajor ?? Currency.DEFAULT_NUMBER_TO_MAJOR
            };
        }
    }
}
