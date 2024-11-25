using FinTrack.Application.Utils;
using FluentValidation;

namespace FinTrack.Application.Authentication.SaveRefreshToken;

public class SaveRefreshTokenValidator: ValidatorBase<SaveRefreshTokenRequest>
{
    public SaveRefreshTokenValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty();
    }
}