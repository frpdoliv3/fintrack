using FinTrack.Domain.Entities;
using FluentValidation;

namespace FinTrack.Application.Authentication.RegisterUser;
public sealed class CreateUserValidator: ValidatorBase<CreateUser>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
