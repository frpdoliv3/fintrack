using FluentValidation;

namespace FinTrack.Application.Country.CreateCountry
{
    public sealed class CreateCountryValidatior : AbstractValidator<CreateCountryRequest>
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
