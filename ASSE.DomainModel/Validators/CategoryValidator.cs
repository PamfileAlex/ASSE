// --------------------------------------------------------------------------------------
// <copyright file="CategoryValidator.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DomainModel.Models;
using FluentValidation;

namespace ASSE.DomainModel.Validators;

/// <summary>
/// Validator for <see cref="Category"/>.
/// </summary>
public class CategoryValidator : AbstractValidator<Category>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CategoryValidator"/> class.
	/// </summary>
	public CategoryValidator()
	{
		RuleFor(x => x.Name).NotEmpty();
	}
}
