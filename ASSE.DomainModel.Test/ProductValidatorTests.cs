using System.Xml.Linq;
using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using FluentAssertions;
using FluentValidation;

namespace ASSE.DomainModel.Test;
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

		var actual = _validator.Validate(product);

		actual.IsValid.Should().BeTrue();
		actual.Errors.Should().BeEmpty();
	}

	[Fact]
	public void Validate_DefaultProduct_Fails()
	{
		var product = new Product();

		var actual = _validator.Validate(product);

		actual.IsValid.Should().BeFalse();
		actual.Errors.Should().HaveCountGreaterThan(0);
	}

	[Fact]
	public void Validate_InvalidCategoryId_Fails()
	{
		var product = new Product()
		{
			Id = 1,
			Name = "Drink",
		};

		var actual = _validator.Validate(product);

		actual.IsValid.Should().BeFalse();
		actual.Errors.Should().HaveCount(1);
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

		var actual = _validator.Validate(product);

		actual.IsValid.Should().BeFalse();
		actual.Errors.Should().HaveCount(1);
	}
}
