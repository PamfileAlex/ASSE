// --------------------------------------------------------------------------------------
// <copyright file="CategoryValidatorTests.cs" company="Transilvania University of Brasov">
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
/// Tests for <see cref="CategoryValidator"/>.
/// </summary>
public class CategoryValidatorTests
{
	private readonly CategoryValidator _validator;

	/// <summary>
	/// Initializes a new instance of the <see cref="CategoryValidatorTests"/> class.
	/// </summary>
	public CategoryValidatorTests()
	{
		_validator = new CategoryValidator();
	}

	/// <summary>
	/// Test for valid <see cref="Category"/>.
	/// </summary>
	[Fact]
	public void Validate_ValidCategory_NoErrors()
	{
		var category = new Category()
		{
			Id = 1,
			ParentId = 1,
			Name = "Electronics",
		};

		var result = _validator.TestValidate(category);

		// result.IsValid.Should().BeTrue();
		result.ShouldNotHaveAnyValidationErrors();
	}

	/// <summary>
	/// Test that default <see cref="Category"/> fails.
	/// </summary>
	[Fact]
	public void Validate_DefaultCategory_Fails()
	{
		var category = new Category();

		var result = _validator.TestValidate(category);

		// result.IsValid.Should().BeFalse();
		result.ShouldHaveAnyValidationError();
	}

	/// <summary>
	/// Test for valid <see cref="Category.ParentId"/>.
	/// </summary>
	[Fact]
	public void Validate_ValidCategoryNoParent_NoErrors()
	{
		var category = new Category()
		{
			Id = 1,
			ParentId = null,
			Name = "Electronics",
		};

		var result = _validator.TestValidate(category);

		// result.IsValid.Should().BeTrue();
		result.ShouldNotHaveAnyValidationErrors();
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
		var category = new Category()
		{
			Name = name,
		};

		var result = _validator.TestValidate(category);

		// result.IsValid.Should().BeFalse();
		result.ShouldHaveValidationErrorFor(category => category.Name);
	}
}
