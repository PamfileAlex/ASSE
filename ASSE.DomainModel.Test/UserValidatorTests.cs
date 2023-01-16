// --------------------------------------------------------------------------------------
// <copyright file="UserValidatorTests.cs" company="Transilvania University of Brasov">
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
/// Tests for <see cref="UserValidator"/>.
/// </summary>
public class UserValidatorTests
{
	private readonly UserValidator _validator;

	/// <summary>
	/// Initializes a new instance of the <see cref="UserValidatorTests"/> class.
	/// </summary>
	public UserValidatorTests()
	{
		_validator = new UserValidator();
	}

	/// <summary>
	/// Test for valid <see cref="User"/>.
	/// </summary>
	[Fact]
	public void Validate_ValidUser_NoErrors()
	{
		var user = new User()
		{
			Id = 1,
			FirstName = "Max",
			LastName = "Verstappen",
			Score = 1,
		};

		var result = _validator.TestValidate(user);

		// result.IsValid.Should().BeTrue();
		result.ShouldNotHaveAnyValidationErrors();
	}

	/// <summary>
	/// Test that default <see cref="User"/> fails.
	/// </summary>
	[Fact]
	public void Validate_DefaultUser_Fails()
	{
		var user = new User();

		var result = _validator.TestValidate(user);

		// result.IsValid.Should().BeFalse();
		result.ShouldHaveAnyValidationError();
	}

	/// <summary>
	/// Test that invalid <see cref="User.FirstName"/> fails.
	/// </summary>
	/// <param name="firstName">Parametrized firstName value.</param>
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	public void Validate_InvalidFirstName_Fails(string firstName)
	{
		var user = new User()
		{
			FirstName = firstName,
			LastName = "Verstappen",
			Score = 1,
		};

		var result = _validator.TestValidate(user);

		// result.IsValid.Should().BeFalse();
		result.ShouldHaveValidationErrorFor(user => user.FirstName);
	}

	/// <summary>
	/// Test that invalid <see cref="User.LastName"/> fails.
	/// </summary>
	/// <param name="lastName">Parametrized lastName value.</param>
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	public void Validate_InvalidLastName_Fails(string lastName)
	{
		var user = new User()
		{
			FirstName = "Max",
			LastName = lastName,
			Score = 1,
		};

		var result = _validator.TestValidate(user);

		// result.IsValid.Should().BeFalse();
		result.ShouldHaveValidationErrorFor(user => user.LastName);
	}

	/// <summary>
	/// Test that invalid <see cref="User.Score"/> fails.
	/// </summary>
	/// <param name="score">Parametrized score value.</param>
	[Theory]
	[InlineData(0.0)]
	[InlineData(-1.0)]
	public void Validate_InvalidScore_Fails(double score)
	{
		var user = new User()
		{
			FirstName = "Max",
			LastName = "Verstappen",
			Score = score,
		};

		var result = _validator.TestValidate(user);

		// result.IsValid.Should().BeFalse();
		result.ShouldHaveValidationErrorFor(user => user.Score);
	}
}