// --------------------------------------------------------------------------------------
// <copyright file="UserRoleValidator.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DomainModel.Models;
using FluentValidation;

namespace ASSE.DomainModel.Validators;

/// <summary>
/// Validator for <see cref="UserRole"/>.
/// </summary>
public class UserRoleValidator : AbstractValidator<UserRole>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="UserRoleValidator"/> class.
	/// </summary>
	public UserRoleValidator()
	{
		RuleFor(x => x.UserId).NotEmpty().GreaterThan(0);
		RuleFor(x => x.RoleId).NotEmpty().GreaterThan(0);
	}
}
