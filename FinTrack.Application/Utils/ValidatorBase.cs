using FluentValidation;

namespace FinTrack.Application.Utils;

public class ValidatorBase<T>: AbstractValidator<T>
{
    protected ValidatorBase() 
    {
        ValidatorOptions.Global.PropertyNameResolver = (type, memberInfo, lambda) =>
        {
            var varName = memberInfo.Name;
            return Char.ToLower(varName[0]) + varName.Substring(1);
        };
    }
}
