// --------------------------------------------------------------------------------------
// <copyright file="ProductValidatorTests.cs" company="Transilvania University of Brasov">
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
/// Tests for <see cref="ProductValidator"/>.
/// </summary>
public class ProductValidatorTests
{
	private readonly ProductValidator _validator;

	/// <summary>
	/// Initializes a new instance of the <see cref="ProductValidatorTests"/> class.
	/// </summary>
	public ProductValidatorTests()
	{
		_validator = new ProductValidator();
	}

	/// <summary>
	/// Test for valid <see cref="Product"/>.
	/// </summary>
	[Fact]
	public void Validate_ValidProduct_NoErrors()
	{
		var product = new Product()
		{
			Id = 1,
			CategoryId = 1,
			Name = "Drink",
		};

		var result = _validator.TestValidate(product);

		// result.IsValid.Should().BeTrue();
		result.ShouldNotHaveAnyValidationErrors();
	}

	/// <summary>
	/// Test that default <see cref="Product"/> fails.
	/// </summary>
	[Fact]
	public void Validate_DefaultProduct_Fails()
	{
		var product = new Product();

		var result = _validator.TestValidate(product);

		// result.IsValid.Should().BeFalse();
		result.ShouldHaveAnyValidationError();
	}

	/// <summary>
	/// Test that invalid <see cref="Product.CategoryId"/> fails.
	/// </summary>
	[Fact]
	public void Validate_InvalidCategoryId_Fails()
	{
		var product = new Product()
		{
			Id = 1,
			Name = "Drink",
		};

		var result = _validator.TestValidate(product);

		// result.IsValid.Should().BeFalse();
		result.ShouldHaveValidationErrorFor(product => product.CategoryId);
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
		var product = new Product()
		{
			CategoryId = 1,
			Name = name,
		};

		var result = _validator.TestValidate(product);

		// result.IsValid.Should().BeFalse();
		result.ShouldHaveValidationErrorFor(product => product.Name);
	}
}
