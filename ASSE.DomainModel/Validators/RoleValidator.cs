// --------------------------------------------------------------------------------------
// <copyright file="RoleValidator.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DomainModel.Models;
using FluentValidation;

namespace ASSE.DomainModel.Validators;

/// <summary>
/// Validator for <see cref="Role"/>.
/// </summary>
public class RoleValidator : AbstractValidator<Role>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="RoleValidator"/> class.
	/// </summary>
	public RoleValidator()
	{
		RuleFor(x => x.Name).NotEmpty();
	}
}
