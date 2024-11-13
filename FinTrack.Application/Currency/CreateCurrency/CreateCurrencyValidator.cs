using FinTrack.Application.Utils;
using FinTrack.Domain.Interfaces;
using FinTrack.Resources;
using FluentValidation;

namespace FinTrack.Application.Currency.CreateCurrency;

/*
 * Making this class internal breaks the reflection necessary to inject valitators into the request pipeline
 */
public sealed class CreateCurrencyValidator: ValidatorBase<CreateCurrencyRequest>
{
    public CreateCurrencyValidator(ICurrencyRepository currencyRepository): base()
    {
        // Rules for Name
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(_ => GeneralMessages.EmptyNameError);

        RuleFor(x => x.Name)
            .MaximumLength(100)
            .WithMessage(_ => CurrencyMessages.OverflowNameError);

        RuleFor(x => x.Name)
            .Must(name =>
            {
                return !currencyRepository.Exists(x => x.Name == name);
            })
            .WithMessage(_ => CurrencyMessages.DuplicateNameError);

        // Rules for Symbol
        RuleFor(x => x.Symbol)
            .MaximumLength(10)
            .WithMessage(_ => CurrencyMessages.SymbolLengthError);

        // Rules for Alpha3Code
        RuleFor(x => x.Alpha3Code)
            .Length(3)
            .WithMessage(_ => CurrencyMessages.Alpha3CodeLengthError);
        
        RuleFor(x => x.Alpha3Code)
            .Must(alpha3Code =>
            {
                return !currencyRepository.Exists(x => x.Alpha3Code == alpha3Code);
            })
            .WithMessage(_ => CurrencyMessages.DuplicateAlpha3CodeError);

        // Rules for Decimals
        RuleFor(x => x.Decimals)
            .GreaterThanOrEqualTo(0)
            .WithMessage(_ => CurrencyMessages.DecimalsMinValueError);
        
        RuleFor(x => x.Decimals)
            .LessThanOrEqualTo(ushort.MaxValue)
            .WithMessage(_ => CurrencyMessages.DecimalsMaxValueError);

        // Rules for NumberToMajor 
        RuleFor(x => x.NumberToMajor)
            .GreaterThanOrEqualTo(0)
            .WithMessage(_ => CurrencyMessages.NumberToMajorMinValueError);
            
        RuleFor(x => x.NumberToMajor)
            .LessThanOrEqualTo(ushort.MaxValue)
            .WithMessage(_ => CurrencyMessages.NumberToMajorMaxValueError);
    }
}
