using System.ComponentModel.DataAnnotations;
using FinTrack.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController(SignInManager<User> signInManager, UserManager<User> userManager, IUserStore<User> userStore) 
{
    private static readonly EmailAddressAttribute _emailAddressAttribute = new();
    
    private static ValidationProblem CreateValidationProblem(IdentityResult result)
    {
        var errorDictionary = new Dictionary<string, string[]>(1);

        foreach (var error in result.Errors)
        {
            string[] newDescriptions;

            if (errorDictionary.TryGetValue(error.Code, out var descriptions))
            {
                newDescriptions = new string[descriptions.Length + 1];
                Array.Copy(descriptions, newDescriptions, descriptions.Length);
                newDescriptions[descriptions.Length] = error.Description;
            }
            else
            {
                newDescriptions = [error.Description];
            }

            errorDictionary[error.Code] = newDescriptions;
        }

        return TypedResults.ValidationProblem(errorDictionary);
    }
    
    [HttpPost]
    [Route("[action]")]
    public async Task<IResult> Login([FromBody] LoginRequest login)
    {
        signInManager.AuthenticationScheme = IdentityConstants.ApplicationScheme;
        var user = await userManager.FindByEmailAsync(login.Identity) ?? await userManager.FindByNameAsync(login.Identity);
        if (user == null)
        {
            return TypedResults.NotFound();
        }
        var result = await signInManager.PasswordSignInAsync(user, login.Password, true, lockoutOnFailure: true);
        
        if (!result.Succeeded)
        {
            return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
        }
        return TypedResults.Empty;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IResult> Register([FromBody] RegisterRequest registration)
    {
        if (!userManager.SupportsUserEmail)
        {
            throw new NotSupportedException($"{nameof(Register)} requires a user store with email support.");
        }
        
        var emailStore = (IUserEmailStore<User>)userStore;
        var email = registration.Email;
    
        if (string.IsNullOrEmpty(email) || !_emailAddressAttribute.IsValid(email))
        {
            return CreateValidationProblem(IdentityResult.Failed(userManager.ErrorDescriber.InvalidEmail(email)));
        }
        
        var user = new User();
        await userStore.SetUserNameAsync(user, registration.UserName, CancellationToken.None);
        await emailStore.SetEmailAsync(user, email, CancellationToken.None);
        var result = await userManager.CreateAsync(user, registration.Password);

        if (!result.Succeeded)
        {
            return CreateValidationProblem(result);
        }

        //await SendConfirmationEmailAsync(user, userManager, context, email);
        return TypedResults.Ok();
    }
    
    [HttpGet]
    [Route("[action]")]
    [Authorize]
    public IResult PingAuth()
    {
        return TypedResults.NoContent();
    }
}

public record LoginRequest
{
    public string Identity { get; init; } = null!;
    public string Password { get; init; } = null!;
}

public record RegisterRequest
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string UserName { get; init; } = null!;
}
