// --------------------------------------------------------------------------------------
// <copyright file="RoleValidatorTests.cs" company="Transilvania University of Brasov">
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
/// Tests for <see cref="RoleValidator"/>.
/// </summary>
public class RoleValidatorTests
{
	private readonly RoleValidator _validator;

	/// <summary>
	/// Initializes a new instance of the <see cref="RoleValidatorTests"/> class.
	/// </summary>
	public RoleValidatorTests()
	{
		_validator = new RoleValidator();
	}

	/// <summary>
	/// Test for valid <see cref="Role"/>.
	/// </summary>
	[Fact]
	public void Validate_ValidRole_NoErrors()
	{
		var role = new Role()
		{
			Id = 1,
			Name = "Admin",
		};

		var result = _validator.TestValidate(role);

		// result.IsValid.Should().BeTrue();
		result.ShouldNotHaveAnyValidationErrors();
	}

	/// <summary>
	/// Test that default <see cref="Role"/> fails.
	/// </summary>
	[Fact]
	public void Validate_DefaultRole_Fails()
	{
		var role = new Role();

		var result = _validator.TestValidate(role);

		// result.IsValid.Should().BeFalse();
		result.ShouldHaveAnyValidationError();
	}

	/// <summary>
	/// Test that invalid <see cref="Category.Name"/> fails.
	/// </summary>
	/// <param name="name">Parametrized name value.</param>
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	public void Validate_InvalidName_Fails(string name)
	{
		var role = new Role()
		{
			Name = name,
		};

		var result = _validator.TestValidate(role);

		// result.IsValid.Should().BeFalse();
		result.ShouldHaveValidationErrorFor(role => role.Name);
	}
}
