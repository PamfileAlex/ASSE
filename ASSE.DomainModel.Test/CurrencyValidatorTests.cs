// --------------------------------------------------------------------------------------
// <copyright file="CurrencyValidatorTests.cs" company="Transilvania University of Brasov">
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
/// Tests for <see cref="CurrencyValidator"/>.
/// </summary>
public class CurrencyValidatorTests
{
	private readonly CurrencyValidator _validator;

	/// <summary>
	/// Initializes a new instance of the <see cref="CurrencyValidatorTests"/> class.
	/// </summary>
	public CurrencyValidatorTests()
	{
		_validator = new CurrencyValidator();
	}

	/// <summary>
	/// Test for valid <see cref="Currency"/>.
	/// </summary>
	[Fact]
	public void Validate_ValidCurrency_NoErrors()
	{
		var currency = new Currency()
		{
			Id = 1,
			Name = "RON",
		};

		var result = _validator.TestValidate(currency);

		// result.IsValid.Should().BeTrue();
		result.ShouldNotHaveAnyValidationErrors();
	}

	/// <summary>
	/// Test that default <see cref="Currency"/> fails.
	/// </summary>
	[Fact]
	public void Validate_DefaultCurrency_Fails()
	{
		var currency = new Currency();

		var result = _validator.TestValidate(currency);

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
		var currency = new Currency()
		{
			Name = name,
		};

		var result = _validator.TestValidate(currency);

		// result.IsValid.Should().BeFalse();
		result.ShouldHaveValidationErrorFor(currency => currency.Name);
	}
}
