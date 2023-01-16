// --------------------------------------------------------------------------------------
// <copyright file="ValidationUtils.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Models;
using FluentValidation;
using FluentValidation.Results;

namespace ASSE.Core.Test;
/// <summary>
/// Static utily class for FluentValidation <see cref="IValidator{T}"/> tests.
/// </summary>
public static class ValidationUtils
{
	private static readonly List<ValidationFailure> _errors = new() { new ValidationFailure() };

	/// <summary>
	/// Gets passing <see cref="ValidationResult"/>.
	/// </summary>
	public static ValidationResult PassingValidationResult { get; } = new ValidationResult();

	/// <summary>
	/// Gets failing <see cref="ValidationResult"/>.
	/// </summary>
	public static ValidationResult FailingValidationResult { get; } = new ValidationResult(_errors);

	/// <summary>
	/// Gets failing <see cref="IValidator{T}"/> for type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">Type of entity that implements <see cref="IKeyEntity"/>.</typeparam>
	/// <returns>Returns failing validator.</returns>
	public static IValidator<T> GetFailingValidator<T>() where T : IKeyEntity
	{
		var validator = new InlineValidator<T>();
		validator.RuleFor(x => x.Id).Must(id => false);
		return validator;
	}
}
