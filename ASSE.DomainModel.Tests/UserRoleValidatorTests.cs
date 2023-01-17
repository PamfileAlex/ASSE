// --------------------------------------------------------------------------------------
// <copyright file="UserRoleValidatorTests.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using FluentValidation.TestHelper;

namespace ASSE.DomainModel.Tests;

/// <summary>
/// Tests for <see cref="UserRoleValidator"/>.
/// </summary>
public class UserRoleValidatorTests
{
	private readonly UserRoleValidator _validator;

	/// <summary>
	/// Initializes a new instance of the <see cref="UserRoleValidatorTests"/> class.
	/// </summary>
	public UserRoleValidatorTests()
	{
		_validator = new UserRoleValidator();
	}

	/// <summary>
	/// Test for valid <see cref="UserRole"/>.
	/// </summary>
	[Fact]
	public void Validate_ValidUserRole_NoErrors()
	{
		var data = new UserRole(1, 1);

		var result = _validator.TestValidate(data);

		result.ShouldNotHaveAnyValidationErrors();
	}

	/// <summary>
	/// Test that default <see cref="UserRole"/> fails.
	/// </summary>
	[Fact]
	public void Validate_DefaultUserRole_Fails()
	{
		var data = default(UserRole);

		var result = _validator.TestValidate(data);

		result.ShouldHaveAnyValidationError();
	}

	/// <summary>
	/// Test that no <see cref="UserRole.UserId"/> fails.
	/// </summary>
	[Fact]
	public void Validate_NoUserId_Fails()
	{
		var data = new UserRole()
		{
			RoleId = 1,
		};

		var result = _validator.TestValidate(data);

		result.ShouldHaveValidationErrorFor(x => x.UserId);
	}

	/// <summary>
	/// Test that no <see cref="UserRole.RoleId"/> fails.
	/// </summary>
	[Fact]
	public void Validate_NoRoleId_Fails()
	{
		var data = new UserRole()
		{
			UserId = 1,
		};

		var result = _validator.TestValidate(data);

		result.ShouldHaveValidationErrorFor(x => x.RoleId);
	}
}
