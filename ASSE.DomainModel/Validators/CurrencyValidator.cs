// --------------------------------------------------------------------------------------
// <copyright file="CurrencyValidator.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DomainModel.Models;
using FluentValidation;

namespace ASSE.DomainModel.Validators;

/// <summary>
/// Validator for <see cref="Currency"/>.
/// </summary>
public class CurrencyValidator : AbstractValidator<Currency>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CurrencyValidator"/> class.
	/// </summary>
	public CurrencyValidator()
	{
		RuleFor(x => x.Name).NotEmpty();
	}
}
