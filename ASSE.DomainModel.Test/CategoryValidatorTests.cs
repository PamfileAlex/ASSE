using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using FluentValidation.TestHelper;

namespace ASSE.DomainModel.Test;
public class CategoryValidatorTests
{
	public readonly CategoryValidator _validator;

	public CategoryValidatorTests()
	{
		_validator = new CategoryValidator();
	}

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

		//result.IsValid.Should().BeTrue();
		result.ShouldNotHaveAnyValidationErrors();
	}

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

		//result.IsValid.Should().BeTrue();
		result.ShouldNotHaveAnyValidationErrors();
	}

	[Fact]
	public void Validate_DefaultCategory_Fails()
	{
		var category = new Category();

		var result = _validator.TestValidate(category);

		//result.IsValid.Should().BeFalse();
		result.ShouldHaveAnyValidationError();
	}

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

		//result.IsValid.Should().BeFalse();
		result.ShouldHaveValidationErrorFor(category => category.Name);
	}
}
