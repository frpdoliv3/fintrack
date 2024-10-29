using FluentValidation;

namespace FinTrack.Application.Utils;

public class HasOwnerIdValidator<T> : ValidatorBase<T> where T : IHasOwnerId
{
    protected HasOwnerIdValidator()
    {
        RuleFor(t => t.OwnerId).NotEmpty();
    }
}