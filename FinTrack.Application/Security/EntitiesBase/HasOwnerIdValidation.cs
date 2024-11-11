using FinTrack.Application.Utils;
using FluentValidation;

namespace FinTrack.Application.Security.EntitiesBase;

public static class HasOwnerIdValidation<T> where T : IHasOwnerId
{
    public static void RegisterOwnerValidation(ValidatorBase<T> validator)
    {
        validator.RuleFor(t => t.OwnerId).NotEmpty();
    }
}
