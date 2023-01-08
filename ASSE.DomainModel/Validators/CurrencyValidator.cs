using ASSE.DomainModel.Models;
using FluentValidation;

namespace ASSE.DomainModel.Validators;
public class CurrencyValidator : AbstractValidator<Currency>
{
	public CurrencyValidator()
	{
		RuleFor(x => x.Name).NotEmpty();
	}
}
