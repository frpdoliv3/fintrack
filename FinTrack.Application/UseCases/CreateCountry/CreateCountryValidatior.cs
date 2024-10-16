using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.UseCases.CreateCountry
{
    public sealed class CreateCountryValidatior: AbstractValidator<CreateCountryRequest>
    {
        public CreateCountryValidatior() 
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Alpha3Code).Length(3);
            RuleFor(x => x.Alpha2Code).Length(2);
            RuleFor(x => x.NumericCode).GreaterThan(0).LessThan(1000);
        }
    }
}
