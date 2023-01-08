using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using FluentAssertions;
using FluentValidation;

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

		var actual = _validator.Validate(category);

		actual.IsValid.Should().BeTrue();
		actual.Errors.Should().BeEmpty();
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

		var actual = _validator.Validate(category);

		actual.IsValid.Should().BeTrue();
		actual.Errors.Should().BeEmpty();
	}

	[Fact]
	public void Validate_DefaultCategory_Fails()
	{
		var category = new Category();

		var actual = _validator.Validate(category);

		actual.IsValid.Should().BeFalse();
		actual.Errors.Should().HaveCountGreaterThan(0);
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

		var actual = _validator.Validate(category);

		actual.IsValid.Should().BeFalse();
		actual.Errors.Should().HaveCount(1);
	}
}
