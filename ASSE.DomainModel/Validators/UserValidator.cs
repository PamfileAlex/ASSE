// --------------------------------------------------------------------------------------
// <copyright file="UserValidator.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DomainModel.Models;
using FluentValidation;

namespace ASSE.DomainModel.Validators;

/// <summary>
/// Validator for <see cref="User"/>.
/// </summary>
public class UserValidator : AbstractValidator<User>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="UserValidator"/> class.
	/// </summary>
	public UserValidator()
	{
		RuleFor(x => x.FirstName).NotEmpty();
		RuleFor(x => x.LastName).NotEmpty();
		RuleFor(x => x.Score).NotEmpty().GreaterThan(0.0);
	}
}
