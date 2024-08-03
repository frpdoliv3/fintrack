using FinTrack.Server.Data;
using FinTrack.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FinTrack.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class SecurityController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;
    private readonly AppDbContext _appDbContext;
    private readonly UserManager<User> _userManager;

    public SecurityController(IAuthorizationService authorizationService, AppDbContext appDbContext, UserManager<User> userManager)
    {
        _authorizationService = authorizationService;
        _appDbContext = appDbContext;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IResult> Create([FromBody]SecurityCreateRequest request)
    {
        throw new NotImplementedException();
    }
}

public record SecurityCreateRequest
{
    [Length(12, 12, ErrorMessage = "Must be exacly 12 characters long")]
    public string ISIN { get; init; } = null!;

    [MinLength(1)]
    public string Name { get; init; } = null!;

    [Length(3, 3, ErrorMessage = "Must be exacly 3 characters long")]
    public string NativeCurrency { get; init; } = null!;

    public List<SecurityTransactionCreateRequest> Transactions { get; init; } = null!;
}

public record SecurityTransactionCreateRequest
{
    [RegularExpression(@"\b(buy|sell)\b", ErrorMessage = "Must be buy or sell (case insensitive)")]
    public string OrderType { get; init; } = null!;

    [Range(1, int.MaxValue)]
    public int Quantity {  get; init; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; init; }
    
    [Range(0, double.MaxValue)]
    public decimal Comission { get; init; }

    [Range(0, double.MaxValue)]
    public decimal ExchangeRate { get; init; }

    [Length(3, 3, ErrorMessage = "Must be exacly 3 characters long")]
    public string Currency { get; init; } = null!;

    public DateTime Date { get; init; }

    //public Test Test { get; init; } = null!;
}

public record Test
{
    [MinLength(1)]
    public string Name { get; init; } = null!;
}
