using ASSE.Core.Models;
using FluentValidation;
using FluentValidation.Results;

namespace ASSE.Core.Test;
public static class ValidationUtils
{
	private static List<ValidationFailure> _errors = new() { new ValidationFailure() };

	public static ValidationResult PassingValidationResult { get; } = new ValidationResult();
	public static ValidationResult FailingValidationResult { get; } = new ValidationResult(_errors);

	public static IValidator<T> GetFailingValidator<T>() where T : IKeyEntity
	{
		var validator = new InlineValidator<T>();
		validator.RuleFor(x => x.Id).Must(id => false);
		return validator;
	}
}
