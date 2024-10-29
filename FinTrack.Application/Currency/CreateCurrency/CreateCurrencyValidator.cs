using FinTrack.Application.Utils;
using FinTrack.Domain.Interfaces;
using FluentValidation;

namespace FinTrack.Application.Currency.CreateCurrency;

/*
 * Making this class internal breaks the reflection necessary to inject valitators into the request pipeline
 */
public sealed class CreateCurrencyValidator: ValidatorBase<CreateCurrencyRequest>
{
    public CreateCurrencyValidator(ICurrencyRepository currencyRepository): base()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MustAsync(async (request, cancellation) =>
            {
                return !await currencyRepository.Exists(x => x.Name == request);
            })
            .WithMessage("Duplicate currency name")
            .MaximumLength(100);

        RuleFor(x => x.Symbol)
            .MaximumLength(10);

        RuleFor(x => x.Alpha3Code)
            .NotEmpty()
            .Length(3)
            .MustAsync(async (request, cancellation) =>
            {
                return !await currencyRepository.Exists(x => x.Alpha3Code == request);
            })
            .WithMessage("Duplicate currency alpha 3 code");

        RuleFor(x => x.Decimals)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(ushort.MaxValue);

        RuleFor(x => x.NumberToMajor)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(ushort.MaxValue);
    }
}
