using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using FluentValidation.TestHelper;

namespace ASSE.DomainModel.Tests;
public class ProductValidatorTests
{
	private readonly ProductValidator _validator;

	public ProductValidatorTests()
	{
		_validator = new ProductValidator();
	}

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

		//result.IsValid.Should().BeTrue();
		result.ShouldNotHaveAnyValidationErrors();
	}

	[Fact]
	public void Validate_DefaultProduct_Fails()
	{
		var product = new Product();

		var result = _validator.TestValidate(product);

		//result.IsValid.Should().BeFalse();
		result.ShouldHaveAnyValidationError();
	}

	[Fact]
	public void Validate_InvalidCategoryId_Fails()
	{
		var product = new Product()
		{
			Id = 1,
			Name = "Drink",
		};

		var result = _validator.TestValidate(product);

		//result.IsValid.Should().BeFalse();
		result.ShouldHaveValidationErrorFor(product => product.CategoryId);
	}

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

		//result.IsValid.Should().BeFalse();
		result.ShouldHaveValidationErrorFor(product => product.Name);
	}
}
