using FluentValidation;

namespace FinTrack.Application;

public class ValidatorBase<T>: AbstractValidator<T>
{
    public ValidatorBase() 
    {
        ValidatorOptions.Global.PropertyNameResolver = (type, memberInfo, lambda) =>
        {
            var varName = memberInfo.Name;
            return Char.ToLower(varName[0]) + varName.Substring(1);
        };
    }
}
