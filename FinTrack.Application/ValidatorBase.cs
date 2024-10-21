using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application
{
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
}
